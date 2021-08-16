using Emmock.Core.Interfaces;
using Emmock.Core.Models;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace Emmock.Persistance
{
	public class FileTestStore : ITestRepository
	{
		private readonly object m_testStoreLock = new object();

		private readonly IFileSystem m_fileSystem;
		private readonly ISerializer m_serializer;

		private readonly List<Test> m_tests;
		private readonly string m_testStorePath;

		public FileTestStore(IFileSystem fileSystem, ISerializer serializer)
		{
			m_fileSystem = fileSystem;
			m_serializer = serializer;

			string currentDirectory = m_fileSystem.Directory.GetCurrentDirectory();
			m_testStorePath = m_fileSystem.Path.GetFullPath(m_fileSystem.Path.Combine(currentDirectory, @"..\..\..\..\..\"));
			m_testStorePath = m_fileSystem.Path.Combine(m_testStorePath, @"DrillSurvData\testStore.json");

			lock (m_testStoreLock)
			{
				if (!m_fileSystem.File.Exists(m_testStorePath))
					m_fileSystem.File.Create(m_testStorePath);
			}

			string data = string.Empty;
			lock (m_testStoreLock)
			{
				data = m_fileSystem.File.ReadAllText(m_testStorePath);
			}

			m_tests = m_serializer.DeserializeObject<List<Test>>(data) ?? new List<Test>();
		}

		public IEnumerable<Test> Tests => m_tests;

		public Test Create(string name, string description, DateTime dueDate)
		{
			Test createdTest = new Test()
			{
				Id = Guid.NewGuid().ToString(),
				Name = name,
				Description = description,
				DueDate = dueDate
			};

			m_tests.Add(createdTest);

			string testData = m_serializer.SerializeObject(m_tests);

			lock (m_testStoreLock)
			{
				m_fileSystem.File.WriteAllText(m_testStorePath, testData);
			}

			return createdTest;
		}

		public IEnumerable<Test> GetAll() => m_tests;

		public void Update(Test testToUpdate)
		{
			int indexOfRig = m_tests.FindIndex(t => t.Id == testToUpdate.Id);
			m_tests[indexOfRig] = testToUpdate;

			string testData = m_serializer.SerializeObject(m_tests);

			lock (m_testStoreLock)
			{
				m_fileSystem.File.WriteAllText(m_testStorePath, testData);
			}
		}
	}
}