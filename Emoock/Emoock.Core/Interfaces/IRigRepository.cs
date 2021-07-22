using Emmock.Core.Models;
using System.Collections.Generic;

namespace Emmock.Core.Interfaces
{
	public interface IRigRepository
	{
		Rig Create(string rigType, string name, string description);
		IEnumerable<Rig> GetAll();
		void Update(Rig rigUnderEdit);
	}
}
