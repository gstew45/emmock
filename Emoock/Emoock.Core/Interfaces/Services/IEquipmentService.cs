using Emmock.Core.Models;

namespace Emmock.Core.Interfaces.Services
{
	public interface IEquipmentService
	{
		Equipment CreateEquipment(string rigId, string name, string type);
		bool UpdateEquipment(Equipment equipmentToUpdate);
	}
}
