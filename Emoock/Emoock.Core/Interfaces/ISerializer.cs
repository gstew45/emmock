namespace Emmock.Core.Interfaces
{
	public interface ISerializer
	{
		string SerializeObject(object value);
		T DeserializeObject<T>(string input);
	}
}
