using Emmock.Persistance.Serializers;
using Emmock.Core.Interfaces;
using Emmock.Core.Models;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace Emmock.Persistance.Tests
{
	[TestClass]
	public class FileRigStoreTests 
	{
		private const string CurrentDirectoryPath = "CurrentDirectory";
		private const string RigStorePath = @"Data\rigStore.json";

		private FileRigStore m_testSubject;
		private IFileSystem m_fileSystem;
		private ISerializer m_serializer;

		private List<Rig> m_expectedRigs;
		private string m_rigStorePath;

		[TestInitialize]
		public void TestInit()
		{
			m_expectedRigs = CreateRigs();

			m_fileSystem = A.Fake<IFileSystem>();
			m_serializer = A.Fake<ISerializer>();

			A.CallTo(() => m_fileSystem.Directory.GetCurrentDirectory()).Returns(CurrentDirectoryPath);

			m_rigStorePath = $"{CurrentDirectoryPath}\\{RigStorePath}";
			A.CallTo(() => m_fileSystem.Path.Combine(CurrentDirectoryPath, RigStorePath)).Returns(m_rigStorePath);

			string rigData = GetRigStoreData(m_expectedRigs);
			A.CallTo(() => m_fileSystem.File.ReadAllText(m_rigStorePath)).Returns(rigData);

			A.CallTo(() => m_serializer.DeserializeObject<List<Rig>>(rigData)).Returns(m_expectedRigs.ToList());

			m_testSubject = new FileRigStore(m_fileSystem, m_serializer);
		}

		[TestMethod]
		public void FileRigStore_Can_Be_Constructed()
		{
			Assert.IsNotNull(m_testSubject);
		}

		[TestMethod]
		public void FileRigStore_Implements_IRigRepository()
		{
			Assert.IsInstanceOfType(m_testSubject, typeof(IRigRepository));
		}

		[TestMethod]
		public void Construction_Builds_Rig_List_From_File()
		{
			CollectionAssert.AreEquivalent(m_expectedRigs, m_testSubject.Rigs.ToList());
		}

		[TestMethod]
		public void Create_ReturnsCreatedRig_With_IdAndType_Populated()
		{
			Rig createdRig = m_testSubject.Create("Jackup", string.Empty, string.Empty);

			Assert.IsFalse(string.IsNullOrEmpty(createdRig.Id));
			Assert.AreEqual("Jackup", createdRig.Type);
		}

		[TestMethod]
		public void Create_CreatedRig_AddedToRigList()
		{
			Rig createdRig = m_testSubject.Create("Jackup", string.Empty, string.Empty);

			Assert.AreEqual(m_expectedRigs.Count + 1, m_testSubject.Rigs.Count());
			Assert.IsTrue(m_testSubject.Rigs.Contains(createdRig));
		}

		[TestMethod]
		public void Create_WritesNewRigList_ToFile()
		{
			string rigStoreData = "TestRigData";
			A.CallTo(() => m_serializer.SerializeObject(A<IEnumerable<Rig>>._)).Returns(rigStoreData);

			Rig createdRig = m_testSubject.Create("Jackup", string.Empty, string.Empty);

			A.CallTo(() => m_fileSystem.File.WriteAllText(m_rigStorePath, rigStoreData)).MustHaveHappenedOnceExactly();
		}

		private string GetRigStoreData(IEnumerable<Rig> rigs)
		{
			return new EmmockJsonSerializer().SerializeObject(rigs);
		}

		private List<Rig> CreateRigs()
		{
			List<Rig> rigs = new List<Rig>();

			for (int i = 0; i < 5; i++)
			{
				rigs.Add(new Rig
				{
					Id = Guid.NewGuid().ToString(),
					Type = "Jackup",
					Description = $"Rig-{i}"
				});
			}

			return rigs;
		}
	}
}
