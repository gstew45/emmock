using Emmock.Application.Interfaces;
using Emmock.Core.Supporting;

namespace Emmock.Application.Models.ToolboxItems
{
	public class MeasurementToolboxItem : ObservableObject, IToolboxItem
	{
		public MeasurementToolboxItem()
		{
		}

		public string Text => "Measurement";
		public string Image => "";
	}
}
