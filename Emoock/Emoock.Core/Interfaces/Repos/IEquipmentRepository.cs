using Emmock.Core.Models;
using System.Collections.Generic;

namespace Emmock.Core.Interfaces
{
	public interface IEquipmentRepository
	{
		Equipment Create(string rigId, string parentId, string name, string type, bool isSystem);
		bool Update(Equipment equipmentToUpdate);
		IEnumerable<Equipment> GetEquipmentByRig(string rigId);
		IEnumerable<Equipment> GetEquipmentByParent(string parentId);
		Equipment GetEquipment(string equipmentId);
	}
}
