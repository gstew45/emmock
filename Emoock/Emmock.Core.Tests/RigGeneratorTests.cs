using Emmock.Core;
using Emmock.Core.Interfaces;
using Emmock.Core.Models;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Emmock.Core.Tests
{
	[TestClass]
	public class RigGeneratorTests
	{
		private RigGenerator m_testSubject;
		private IRigRepository m_rigRepo;
		private IEquipmentRepository m_equipmentRepo;
		private IRigTemplateRepository m_rigTemplateRepo;

		[TestInitialize]
		public void TestInit()
		{
			m_rigRepo = A.Fake<IRigRepository>();
			m_equipmentRepo = A.Fake<IEquipmentRepository>();
			m_rigTemplateRepo = A.Fake<IRigTemplateRepository>();

			m_testSubject = new RigGenerator(m_rigRepo, m_equipmentRepo, m_rigTemplateRepo);
		}

		[TestMethod]
		public void RigGenerator_Can_Be_Constructed()
		{
			Assert.IsNotNull(m_testSubject);
		}

		[TestMethod]
		public void GenerateRig_GetsRigTemplate_From_TemplateRepo()
		{
			m_testSubject.GenerateRig("Jackup", string.Empty, string.Empty);

			A.CallTo(() => m_rigTemplateRepo.GetRigTemplate("Jackup")).MustHaveHappenedOnceExactly();
		}

		[TestMethod]
		public void GenerateRig_CreatesRig()
		{
			m_testSubject.GenerateRig("Jackup", string.Empty, string.Empty);

			A.CallTo(() => m_rigRepo.Create("Jackup", string.Empty, string.Empty)).MustHaveHappenedOnceExactly();
		}

		[TestMethod]
		public void GenerateRig_CreatesEquipment_For_Every_EquipmentTemplate_In_RigTemplate()
		{
			RigTemplate rigTemplate = GetRigTemplate();
			A.CallTo(() => m_rigTemplateRepo.GetRigTemplate("Jackup")).Returns(rigTemplate);

			Rig rig = new Rig() { Id = Guid.NewGuid().ToString() };
			A.CallTo(() => m_rigRepo.Create("Jackup", string.Empty, string.Empty)).Returns(rig);

			m_testSubject.GenerateRig("Jackup", string.Empty, string.Empty);

			foreach (EquipmentTemplate equipmentTemplate in rigTemplate.EquipmentTemplates)
			{
				A.CallTo(() => m_equipmentRepo.Create(rig.Id, null, equipmentTemplate.Name, equipmentTemplate.Type, false)).MustHaveHappenedOnceExactly();
			}
		}

		[TestMethod]
		public void GenerateRig_AddsFields_From_EquipmentTemplate_To_Equipment_Correctly()
		{
			// arrange
			RigTemplate rigTemplate = GetRigTemplate();
			A.CallTo(() => m_rigTemplateRepo.GetRigTemplate("Jackup")).Returns(rigTemplate);

			Rig rig = new Rig() { Id = Guid.NewGuid().ToString() };
			A.CallTo(() => m_rigRepo.Create("Jackup", string.Empty, string.Empty)).Returns(rig);

			List<Equipment> createdEquipmentList = new List<Equipment>();
			foreach (EquipmentTemplate equipmentTemplate in rigTemplate.EquipmentTemplates)
			{
				Equipment equipment = new Equipment() { Id = equipmentTemplate.Name };
				A.CallTo(() => m_equipmentRepo.Create(rig.Id, null, equipmentTemplate.Name, equipmentTemplate.Type, false)).Returns(equipment);

				createdEquipmentList.Add(equipment);
			}

			// act
			m_testSubject.GenerateRig("Jackup", string.Empty, string.Empty);

			// assert
			foreach (EquipmentTemplate equipmentTemplate in rigTemplate.EquipmentTemplates)
			{
				Equipment currentEquipment = createdEquipmentList.FirstOrDefault(e => e.Id == equipmentTemplate.Name);

				CollectionAssert.AreEqual(equipmentTemplate.Fields, currentEquipment.Fields);
			}
		}

		[TestMethod]
		public void GenerateRig_UpdatesEquipment_For_Every_EquipmentCreated()
		{
			// arrange
			RigTemplate rigTemplate = GetRigTemplate();
			A.CallTo(() => m_rigTemplateRepo.GetRigTemplate("Jackup")).Returns(rigTemplate);

			Rig rig = new Rig() { Id = Guid.NewGuid().ToString() };
			A.CallTo(() => m_rigRepo.Create("Jackup", string.Empty, string.Empty)).Returns(rig);

			List<Equipment> createdEquipmentList = new List<Equipment>();
			foreach (EquipmentTemplate equipmentTemplate in rigTemplate.EquipmentTemplates)
			{
				Equipment equipment = new Equipment() { Id = equipmentTemplate.Name };
				A.CallTo(() => m_equipmentRepo.Create(rig.Id, null, equipmentTemplate.Name, equipmentTemplate.Type, false)).Returns(equipment);

				createdEquipmentList.Add(equipment);
			}

			// act
			m_testSubject.GenerateRig("Jackup", string.Empty, string.Empty);

			// assert
			foreach (EquipmentTemplate equipmentTemplate in rigTemplate.EquipmentTemplates)
			{
				Equipment currentEquipment = createdEquipmentList.FirstOrDefault(e => e.Id == equipmentTemplate.Name);

				A.CallTo(() => m_equipmentRepo.Update(currentEquipment)).MustHaveHappenedOnceExactly();
			}
		}

		[TestMethod]
		public void GenerateRig_ReturnsGeneratedRig()
		{
			RigTemplate rigTemplate = GetRigTemplate();
			A.CallTo(() => m_rigTemplateRepo.GetRigTemplate("Jackup")).Returns(rigTemplate);

			Rig expectedRig = new Rig() { Id = Guid.NewGuid().ToString() };
			A.CallTo(() => m_rigRepo.Create("Jackup", string.Empty, string.Empty)).Returns(expectedRig);

			Rig rig = m_testSubject.GenerateRig("Jackup", string.Empty, string.Empty);

			Assert.AreEqual(expectedRig.Id, rig.Id);
		}

		private RigTemplate GetRigTemplate()
		{
			RigTemplate rigTemplate = new RigTemplate();

			for (int i = 0; i < 5; i++)
			{
				EquipmentTemplate equipmentTemplate = new EquipmentTemplate();
				equipmentTemplate.Name = $"EquipmentTemplate-{i}";
				equipmentTemplate.Type = $"EquipmentType-{i}";
				equipmentTemplate.Fields.Add(new DateTimeField());
				equipmentTemplate.Fields.Add(new DateTimeField());
				equipmentTemplate.Fields.Add(new DateTimeField());

				rigTemplate.EquipmentTemplates.Add(equipmentTemplate);
			}

			return rigTemplate;
		}
	}
}
