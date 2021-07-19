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
	public class FileEquipmentStoreTests 
	{
		private const string CurrentDirectoryPath = "CurrentDirectory";
		private const string EquipmentStorePath = @"Data\equipmentStore.json";

		private FileEquipmentStore m_testSubject;
		private IFileSystem m_fileSystem;
		private ISerializer m_serializer;

		private List<Equipment> m_expectedEquipment;
		private string m_equipmentStorePath;

		[TestInitialize]
		public void TestInit()
		{
			m_expectedEquipment = CreateEquipment();

			m_fileSystem = A.Fake<IFileSystem>();
			m_serializer = A.Fake<ISerializer>();

			A.CallTo(() => m_fileSystem.Directory.GetCurrentDirectory()).Returns(CurrentDirectoryPath);

			m_equipmentStorePath = $"{CurrentDirectoryPath}\\{EquipmentStorePath}";
			A.CallTo(() => m_fileSystem.Path.Combine(CurrentDirectoryPath, EquipmentStorePath)).Returns(m_equipmentStorePath);

			string equipmentData = GetEquipmentStoreData(m_expectedEquipment);
			A.CallTo(() => m_fileSystem.File.ReadAllText(m_equipmentStorePath)).Returns(equipmentData);

			A.CallTo(() => m_serializer.DeserializeObject<List<Equipment>>(equipmentData)).Returns(m_expectedEquipment.ToList());

			m_testSubject = new FileEquipmentStore(m_fileSystem, m_serializer);
		}

		[TestMethod]
		public void FileEquipmentStore_Can_Be_Constructed()
		{
			Assert.IsNotNull(m_testSubject);
		}

		[TestMethod]
		public void FileEquipmentStore_Implements_IRigRepository()
		{
			Assert.IsInstanceOfType(m_testSubject, typeof(IEquipmentRepository));
		}

		[TestMethod]
		public void Construction_Builds_Equipment_List_From_File()
		{
			CollectionAssert.AreEquivalent(m_expectedEquipment, m_testSubject.Equipment.ToList());
		}

		[TestMethod]
		public void Create_ReturnsCreatedEquipment_With_IdAndRigId_Populated()
		{
			string expectedRigId = "TestRigId";
			Equipment createdEquipment = m_testSubject.Create(expectedRigId, null, "TestEquipmentName", "TestEquipmentType", false);

			Assert.IsFalse(string.IsNullOrEmpty(createdEquipment.Id));
			Assert.AreEqual(expectedRigId, createdEquipment.RigId);
		}

		[TestMethod]
		public void Create_CreatedEquipment_AddedToEquipmentList()
		{
			string expectedRigId = "TestRigId";
			Equipment createdEquipment = m_testSubject.Create(expectedRigId, null, "TestEquipmentName", "TestEquipmentType", false);

			Assert.AreEqual(m_expectedEquipment.Count + 1, m_testSubject.Equipment.Count());
			Assert.IsTrue(m_testSubject.Equipment.Contains(createdEquipment));
		}

		[TestMethod]
		public void Create_WritesNewEquipmentList_ToFile()
		{
			string equipmentStoreData = "TestEquipmentData";
			A.CallTo(() => m_serializer.SerializeObject(A<IEnumerable<Equipment>>._)).Returns(equipmentStoreData);

			string expectedRigId = "TestRigId";
			Equipment createdEquipment = m_testSubject.Create(expectedRigId, null, "TestEquipmentName", "TestEquipmentType", false);

			A.CallTo(() => m_fileSystem.File.WriteAllText(m_equipmentStorePath, equipmentStoreData)).MustHaveHappenedOnceExactly();
		}

		private string GetEquipmentStoreData(IEnumerable<Equipment> equipment)
		{
			return new EmmockJsonSerializer().SerializeObject(equipment);
		}

		private List<Equipment> CreateEquipment()
		{
			List<Equipment> equipment = new List<Equipment>();

			for (int i = 0; i < 5; i++)
			{
				equipment.Add(new Equipment
				{
					Id = Guid.NewGuid().ToString(),
					RigId = $"Rig-{i}"
				});
			}

			return equipment;
		}
	}
}
