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
			m_equipmentStorePath = m_fileSystem.Path.GetFullPath(m_fileSystem.Path.Combine(currentDirectory, @"..\..\..\..\..\"));
			m_equipmentStorePath = m_fileSystem.Path.Combine(m_equipmentStorePath, @"DrillSurvData\equipmentStore.json");

			lock (m_equipmentStoreLock)
			{
				if (!m_fileSystem.File.Exists(m_equipmentStorePath))
					m_fileSystem.File.Create(m_equipmentStorePath);
			}

			string data = string.Empty;

			lock (m_equipmentStoreLock)
			{
				data = m_fileSystem.File.ReadAllText(m_equipmentStorePath);
			}

			m_equipment = m_serializer.DeserializeObject<List<Equipment>>(data) ?? new List<Equipment>();
		}

		public IEnumerable<Equipment> Equipment => m_equipment;

		public Equipment Create(string rigId, string parentId, string name, string type, bool isSystem)
		{
			Equipment createdEquipment = new Equipment()
			{
				Id = Guid.NewGuid().ToString(),
				RigId = rigId,
				ParentEquipmentId = parentId,
				Name = name,
				Type = type,
				IsSystem = isSystem
			};

			m_equipment.Add(createdEquipment);

			CommitChangesToFile();

			return createdEquipment;
		}

		public Equipment GetEquipment(string equipmentId)
		{
			Equipment equipment = Equipment.FirstOrDefault(e => e.Id == equipmentId);

			// Get copy of object so we don't update the actual object in the store until Update is called.
			string data = m_serializer.SerializeObject(equipment);
			Equipment copiedObject = m_serializer.DeserializeObject<Equipment>(data);

			return copiedObject;
		}

		public IEnumerable<Equipment> GetEquipmentByParent(string parentId)
		{
			return Equipment.Where(e => e.ParentEquipmentId == parentId).ToList();
		}

		public IEnumerable<Equipment> GetEquipmentByRig(string rigId)
		{
			return Equipment.Where(e => e.RigId == rigId).ToList();
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
