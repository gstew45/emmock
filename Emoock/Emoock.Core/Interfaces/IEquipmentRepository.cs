using Emmock.Core.Models;

namespace Emmock.Core.Interfaces
{
	public interface IEquipmentRepository
	{
		Equipment Create(string rigId, string name, string type);
		bool Update(Equipment equipmentToUpdate);
	}
}
