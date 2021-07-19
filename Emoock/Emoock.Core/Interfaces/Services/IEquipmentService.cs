using Emmock.Core.Models;
using System.Collections.Generic;

namespace Emmock.Core.Interfaces.Services
{
	public interface IEquipmentService
	{
		Equipment CreateEquipment(string rigId, string parentId, string name, string type, bool isSystem);
		bool UpdateEquipment(Equipment equipmentToUpdate);
		IEnumerable<Equipment> GetEquipmentByRigId(string rigId);
		IEnumerable<Equipment> GetEquipmentByParentId(string parentId);
		Equipment GetEquipmentById(string equipmentId);
	}
}
