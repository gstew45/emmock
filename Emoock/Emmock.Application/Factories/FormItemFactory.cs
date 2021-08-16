using Emmock.Application.Interfaces;
using Emmock.Application.Models.FormItems;
using Emmock.Application.Models.ToolboxItems;

namespace Emmock.Application.Factories
{
	public class FormItemFactory : IFormItemFactory
	{
		public IFormItem GetFormItem(IToolboxItem toolboxItem)
		{
			return toolboxItem switch
			{
				MeasurementToolboxItem measurement => CreateMeasurementFormItem(),
				_ => null
			};
		}

		private IFormItem CreateMeasurementFormItem()
		{
			return new MeasurementFormItem();
		}
	}
}
