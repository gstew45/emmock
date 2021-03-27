using Emmock.Core;
using Emmock.Core.Interfaces;
using Emmock.Core.Models;
using Emmock.Persistance;
using Emmock.Persistance.Serializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO.Abstractions;
using System.Linq;

namespace Emmock.Integration.Tests
{
	[TestClass]
	public class GenerateRigToFileStoreTests
	{
		[TestMethod]
		public void GenerateJackupRig_RigAndEquipment_Created_Correctly()
		{
			// arrange
			IFileSystem fIleSystem = new FileSystem();
			ISerializer serializer = new EmmockJsonSerializer();

			FileRigStore rigRepo = new FileRigStore(fIleSystem, serializer);
			FileEquipmentStore equipmentRepo = new FileEquipmentStore(fIleSystem, serializer);
			IRigTemplateRepository rigTemplateRepo = new FileRigTemplateStore(fIleSystem, serializer);

			RigTemplate rigTemplate = rigTemplateRepo.GetRigTemplate("Jackup");

			// act
			RigGenerator rigGenerator = new RigGenerator(rigRepo, equipmentRepo, rigTemplateRepo);
			rigGenerator.GenerateRig("Jackup");

			// assert
			Assert.AreEqual(1, rigRepo.Rigs.Count());
			Assert.AreEqual(rigTemplate.EquipmentTemplates.Count, equipmentRepo.Equipment.Count());

			// TODO: Much deeper check around rig properties from generated rig
			// TODO: Much deeper check around equipment created from generator
		}
	}
}
