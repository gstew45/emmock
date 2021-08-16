using Emmock.Application.Interfaces;

namespace Emmock.Application.Messaging
{
	internal class FormSurfaceItemSelectionMessage
	{
		public FormSurfaceItemSelectionMessage(IFormItem selectedFormItem)
		{
			SelectedFormItem = selectedFormItem;
		}

		public IFormItem SelectedFormItem { get; }
	}
}