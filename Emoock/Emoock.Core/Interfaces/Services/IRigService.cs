using Emmock.Core.Models;
using System.Collections.Generic;

namespace Emmock.Core.Interfaces.Services
{
	public interface IRigService
	{
		Rig CreateRig(string rigType);

		Rig GenerateRig(string rigType);

		IEnumerable<Rig> GetAllRigs();
	}
}
