using Emmock.Core.Interfaces;
using Emmock.Core.Models;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;

namespace Emmock.Persistance
{
	public class FileRigStore : IRigRepository
	{
		private readonly object m_rigStoreLock = new object();

		private readonly IFileSystem m_fileSystem;
		private readonly ISerializer m_serializer;

		private readonly List<Rig> m_rigs;
		private readonly string m_rigStorePath;

		public FileRigStore(IFileSystem fileSystem, ISerializer serializer)
		{
			m_fileSystem = fileSystem;
			m_serializer = serializer;

			string currentDirectory = m_fileSystem.Directory.GetCurrentDirectory();
			m_rigStorePath = m_fileSystem.Path.GetFullPath(m_fileSystem.Path.Combine(currentDirectory, @"..\..\..\..\..\"));
			m_rigStorePath = m_fileSystem.Path.Combine(m_rigStorePath, @"DrillSurvData\rigStore.json");

			lock (m_rigStoreLock)
			{
				if (!m_fileSystem.File.Exists(m_rigStorePath))
					m_fileSystem.File.Create(m_rigStorePath);
			}

			string data = string.Empty;
			lock (m_rigStoreLock)
			{
				data = m_fileSystem.File.ReadAllText(m_rigStorePath);
			}

			m_rigs = m_serializer.DeserializeObject<List<Rig>>(data) ?? new List<Rig>();
		}

		public IEnumerable<Rig> Rigs => m_rigs;

		public Rig Create(string rigType, string name, string description)
		{
			Rig createdRig = new Rig()
			{
				Id = Guid.NewGuid().ToString(),
				Name = name,
				Description = description,
				Type = rigType
			};

			m_rigs.Add(createdRig);

			string rigData = m_serializer.SerializeObject(m_rigs);

			lock (m_rigStoreLock)
			{
				m_fileSystem.File.WriteAllText(m_rigStorePath, rigData);
			}

			return createdRig;
		}

		public IEnumerable<Rig> GetAll() => m_rigs;
	}
}