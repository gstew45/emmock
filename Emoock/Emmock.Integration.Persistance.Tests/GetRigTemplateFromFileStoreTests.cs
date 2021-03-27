using Emmock.Persistance;
using Emmock.Persistance.Serializers;
using Emoock.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO.Abstractions;

namespace Emmock.Integration.Persistance.Tests
{
	[TestClass]
	public class GetRigTemplateFromFileStoreTests
	{
		private FileRigTemplateStore m_testSubject;

		[TestInitialize]
		public void TestInit()
		{
			m_testSubject = new FileRigTemplateStore(new FileSystem(), new EmmockJsonSerializer());
		}

		[TestMethod]
		public void GetJackupRigTemplate_RigTemplate_Contains_Correct_JackupEquipment()
		{
			RigTemplate jackupTemplate = m_testSubject.GetRigTemplate("Jackup");

			Assert.AreEqual(1, jackupTemplate.EquipmentTemplates.Count);
		}
	}
}
