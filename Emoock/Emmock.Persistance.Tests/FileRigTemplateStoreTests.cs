using Emmock.Core.Interfaces;
using Emmock.Core.Models;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO.Abstractions;

namespace Emmock.Persistance.Tests
{
	[TestClass]
	public class FileRigTemplateStoreTests
	{
		private FileRigTemplateStore m_testSubject;
		private IFileSystem m_fileSystem;
		private ISerializer m_serializer;

		[TestInitialize]
		public void TestInit()
		{
			m_fileSystem = A.Fake<IFileSystem>();
			m_serializer = A.Fake<ISerializer>();

			m_testSubject = new FileRigTemplateStore(m_fileSystem, m_serializer);
		}

		[TestMethod]
		public void FileRigTemplateStore_Can_Be_Constructed()
		{
			Assert.IsNotNull(m_testSubject);
		}

		[TestMethod]
		public void FileRigTemplateStore_Implements_IRigTemplateRepository()
		{
			Assert.IsInstanceOfType(m_testSubject, typeof(IRigTemplateRepository));
		}

		[TestMethod]
		[DataRow("Jackup", "jackupRigTemplate.json", DisplayName = "RigType is Jackup")]
		[DataRow("Platform", "platformRigTemplate.json", DisplayName = "RigType is Platform")]
		public void GetRigTemplate_Constructs_Correct_FilePath_For_RigType(string rigType, string rigTemplateFileName)
		{
			string currentDirectory = "CurrentDirectory";
			A.CallTo(() => m_fileSystem.Directory.GetCurrentDirectory()).Returns(currentDirectory);

			m_testSubject.GetRigTemplate(rigType);

			A.CallTo(() => m_fileSystem.Path.Combine(currentDirectory, "RigTemplates", rigTemplateFileName)).MustHaveHappenedOnceExactly();
		}

		[TestMethod]
		public void GetRigTemplate_ReadsTemplateData_From_File()
		{
			string currentDirectory = "CurrentDirectory";
			A.CallTo(() => m_fileSystem.Directory.GetCurrentDirectory()).Returns(currentDirectory);

			string expectedTemplatePath = @"CurrentDirectory\RigTemplates\jackupRigTemplate.json";
			A.CallTo(() => m_fileSystem.Path.Combine(currentDirectory, "RigTemplates", "jackupRigTemplate.json")).Returns(expectedTemplatePath);

			m_testSubject.GetRigTemplate("Jackup");

			A.CallTo(() => m_fileSystem.File.ReadAllText(expectedTemplatePath)).MustHaveHappenedOnceExactly();
		}

		[TestMethod]
		public void GetRigTemplate_Deserializes_RigTemplate_From_Data_Returns_RigTemplate()
		{
			string expectedTemplatePath = @"RigTemplates\jackupRigTemplate.json";
			A.CallTo(() => m_fileSystem.Path.Combine("RigTemplates", "jackupRigTemplate.json")).Returns(expectedTemplatePath);
			A.CallTo(() => m_fileSystem.File.ReadAllText(expectedTemplatePath)).Returns(GetRigTemplateData());

			RigTemplate rigTemplate = m_testSubject.GetRigTemplate("Jackup");

			Assert.IsNotNull(rigTemplate);
		}

		private string GetRigTemplateData()
		{
			return "{\"EquipmentTemplates\":[{\"Name\":null,\"Type\":null,\"Fields\":[{\"$type\":\"Emoock.Core.Models.DateTimeField, Emoock.Core\",\"Type\":\"DateTime\",\"Name\":null,\"TypedValue\":\"0001-01-01T00:00:00\"},{\"$type\":\"Emoock.Core.Models.DateTimeField, Emoock.Core\",\"Type\":\"DateTime\",\"Name\":null,\"TypedValue\":\"0001-01-01T00:00:00\"}]}]}";
		}
	}
}
