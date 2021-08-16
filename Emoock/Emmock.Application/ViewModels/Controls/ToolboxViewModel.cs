using Emmock.Application.Interfaces;
using Emmock.Application.Models.ToolboxItems;
using Emmock.Core.Supporting;
using System.Collections.ObjectModel;

namespace Emmock.Application.ViewModels.Controls
{
	public class ToolboxViewModel : ObservableObject, IToolboxViewModel
	{
		public ToolboxViewModel()
		{
		}

		public ObservableCollection<IToolboxItem> ToolboxItems { get; } = new ObservableCollection<IToolboxItem>();

		public void Initialize()
		{
			ToolboxItems.Add(new MeasurementToolboxItem());
		}
	}
}
