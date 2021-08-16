namespace Emmock.Application.Interfaces
{
	public interface IFormItemFactory
	{
		IFormItem GetFormItem(IToolboxItem toolboxItem);
	}
}
