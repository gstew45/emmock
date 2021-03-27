using Emmock.Core.Interfaces;
using Newtonsoft.Json;

namespace Emmock.Persistance.Serializers
{
	public class EmmockJsonSerializer : ISerializer
	{
		private JsonSerializerSettings m_settings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.Auto
		};

		public T DeserializeObject<T>(string input)
		{
			return JsonConvert.DeserializeObject<T>(input, m_settings);
		}

		public string SerializeObject(object value)
		{
			return JsonConvert.SerializeObject(value, m_settings);
		}
	}
}
