using Emmock.Application.ViewModels.Controls;
using System.Collections.ObjectModel;

namespace Emmock.Application.Interfaces
{
	public interface IToolboxViewModel
	{
		ObservableCollection<IToolboxItem> ToolboxItems { get; }
		void Initialize();
	}
}
