using Emmock.Core.Models;

namespace Emmock.Core.Interfaces
{
	public interface IRigTemplateRepository
	{
		RigTemplate GetRigTemplate(string rigType);
	}
}
