using Emmock.Core.Interfaces;
using Emmock.Core.Models;
using System.IO.Abstractions;

namespace Emmock.Persistance
{
	public class FileRigTemplateStore : IRigTemplateRepository
	{
		private readonly object m_templateStoreLock = new object();

		private readonly IFileSystem m_fileSystem;
		private readonly ISerializer m_serializer;
		private readonly string m_rigTemplatePath = @"Data\RigTemplates";

		public FileRigTemplateStore(IFileSystem fileSystem, ISerializer serializer)
		{
			m_fileSystem = fileSystem;
			m_serializer = serializer;
		}

		public RigTemplate GetRigTemplate(string rigType)
		{
			string fullRigTemplatePath = GetRigTemplatePathByType(rigType);

			string data = string.Empty;

			lock (m_templateStoreLock)
			{
				data = m_fileSystem.File.ReadAllText(fullRigTemplatePath);
			}

			return m_serializer.DeserializeObject<RigTemplate>(data);
		}

		private string GetRigTemplatePathByType(string rigType)
		{
			string templateName = rigType == "Jackup" ? "jackupRigTemplate.json" : "platformRigTemplate.json";

			string currentDirectory = m_fileSystem.Directory.GetCurrentDirectory();
			return m_fileSystem.Path.Combine(currentDirectory, m_rigTemplatePath, templateName);
		}
	}
}
