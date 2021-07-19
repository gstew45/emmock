using Emmock.Core.Models;
using System.Collections.Generic;

namespace Emmock.Core.Interfaces.Services
{
	public interface IRigService
	{
		Rig CreateRig(string rigType, string name, string description);

		Rig GenerateRig(string rigType, string name, string description);

		IEnumerable<Rig> GetAllRigs();
		Rig GetRigById(string rigId);
	}
}
