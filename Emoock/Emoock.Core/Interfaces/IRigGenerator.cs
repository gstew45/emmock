using Emmock.Core.Models;

namespace Emmock.Core.Interfaces
{
	public interface IRigGenerator
	{
		Rig GenerateRig(string rigtype, string name, string description);
	}
}
