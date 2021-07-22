namespace Emmock.Core.Interfaces
{
	public interface IField : ICopy<IField>
	{
		string Name { get; set; }
		string Type { get; }
		object GetValue();
		void SetValue(object value);
	}
}
