using System.Windows.Input;

namespace Emmock.Application.Interfaces
{
	public interface IFormSurfaceViewModel
	{
		ICommand CreateFormItemCommand { get; }
		ICommand SelectFormItemCommand { get; }
	}

}
