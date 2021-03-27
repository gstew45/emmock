using Emmock.Core.Models;

namespace Emmock.Core.Interfaces
{
	public interface IRigRepository
	{
		Rig Create(string rigType);
	}
}
