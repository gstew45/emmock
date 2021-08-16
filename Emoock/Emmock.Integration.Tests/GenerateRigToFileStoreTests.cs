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

			IRigRepository rigRepo = new FileRigStore(fIleSystem, serializer);
			IEquipmentRepository equipmentRepo = new FileEquipmentStore(fIleSystem, serializer);
			IRigTemplateRepository rigTemplateRepo = new FileRigTemplateStore(fIleSystem, serializer);

			RigTemplate rigTemplate = rigTemplateRepo.GetRigTemplate("Jackup");

			// act
			RigGenerator rigGenerator = new RigGenerator(rigRepo, equipmentRepo, rigTemplateRepo);
			Rig generatedRig = rigGenerator.GenerateRig("Jackup", "Rig For Test", "");

			Rig actualRig = rigRepo.GetAll().FirstOrDefault(r => r.Id == generatedRig.Id);

			// assert
			Assert.IsNotNull(actualRig);
			Assert.AreEqual(generatedRig.Name, actualRig.Name);

			// TODO: Much deeper check around equipment created from generator
		}
	}
}
