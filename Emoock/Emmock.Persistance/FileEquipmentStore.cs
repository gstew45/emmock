using Emmock.Core.Interfaces;
using Emmock.Core.Models;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

namespace Emmock.Persistance
{
	public class FileEquipmentStore : IEquipmentRepository
	{
		private readonly object m_equipmentStoreLock = new object();

		private readonly IFileSystem m_fileSystem;
		private readonly ISerializer m_serializer;

		private readonly List<Equipment> m_equipment;
		private readonly string m_equipmentStorePath;

		public FileEquipmentStore(IFileSystem fileSystem, ISerializer serializer)
		{
			m_fileSystem = fileSystem;
			m_serializer = serializer;

			string currentDirectory = m_fileSystem.Directory.GetCurrentDirectory();
			m_equipmentStorePath = m_fileSystem.Path.Combine(currentDirectory, @"Data\equipmentStore.json");

			string data = string.Empty;

			lock (m_equipmentStoreLock)
			{
				data = m_fileSystem.File.ReadAllText(m_equipmentStorePath);
			}

			m_equipment = m_serializer.DeserializeObject<List<Equipment>>(data) ?? new List<Equipment>();
		}

		public IEnumerable<Equipment> Equipment => m_equipment;

		public Equipment Create(string rigId, string name, string type)
		{
			Equipment createdEquipment = new Equipment()
			{
				Id = Guid.NewGuid().ToString(),
				RigId = rigId
			};

			m_equipment.Add(createdEquipment);

			CommitChangesToFile();

			return createdEquipment;
		}

		public bool Update(Equipment equipmentToUpdate)
		{
			Equipment equipment = m_equipment.FirstOrDefault(e => e.Id == equipmentToUpdate.Id);
			if (equipment == null)
				return false;

			int itemToReplaceIndex = m_equipment.IndexOf(equipment);
			m_equipment[itemToReplaceIndex] = equipmentToUpdate;

			CommitChangesToFile();

			return true;
		}

		private void CommitChangesToFile()
		{
			string equipmentData = m_serializer.SerializeObject(m_equipment);

			lock (m_equipmentStoreLock)
			{
				m_fileSystem.File.WriteAllText(m_equipmentStorePath, equipmentData);
			}
		}
	}
}
