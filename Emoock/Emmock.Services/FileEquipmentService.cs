using Emmock.Core.Interfaces;
using Emmock.Core.Interfaces.Services;
using Emmock.Core.Models;

namespace Emmock.Services
{
	public class FileEquipmentService : IEquipmentService
	{
		private readonly IEquipmentRepository m_equipmentRepo;

		public FileEquipmentService(IEquipmentRepository equipmentRepo)
		{
			m_equipmentRepo = equipmentRepo;
		}

		public Equipment CreateEquipment(string rigId, string name, string type) => m_equipmentRepo.Create(rigId, name, type);

		public bool UpdateEquipment(Equipment equipmentToUpdate) => m_equipmentRepo.Update(equipmentToUpdate);
	}
}
