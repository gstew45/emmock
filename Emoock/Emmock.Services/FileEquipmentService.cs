using Emmock.Core.Interfaces;
using Emmock.Core.Interfaces.Services;
using Emmock.Core.Models;
using System.Collections.Generic;

namespace Emmock.Services
{
	public class FileEquipmentService : IEquipmentService
	{
		private readonly IEquipmentRepository m_equipmentRepo;

		public FileEquipmentService(IEquipmentRepository equipmentRepo)
		{
			m_equipmentRepo = equipmentRepo;
		}

		public Equipment CreateEquipment(string rigId, string parentId, string name, string type, bool isSystem) => m_equipmentRepo.Create(rigId, parentId, name, type, isSystem);

		public bool UpdateEquipment(Equipment equipmentToUpdate) => m_equipmentRepo.Update(equipmentToUpdate);

		public IEnumerable<Equipment> GetEquipmentByRigId(string rigId) => m_equipmentRepo.GetEquipmentByRig(rigId);

		public Equipment GetEquipmentById(string equipmentId) => m_equipmentRepo.GetEquipment(equipmentId);

		public IEnumerable<Equipment> GetEquipmentByParentId(string parentId) => m_equipmentRepo.GetEquipmentByParent(parentId);
	}
}
