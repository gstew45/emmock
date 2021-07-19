namespace Emmock.Core.Interfaces
{
	public interface IField
	{
		string Name { get; set; }
		string Type { get; }
		object GetValue();
		void SetValue(object value);
	}
}
