using Emmock.Core.Supporting;
namespace Emmock.Application.Models.Properties
{
	public class PropertyValue : ObservableObject
	{
		public PropertyValue(string name, string displayName)
		{
			Name = name;
			DisplayName = displayName;
		}

		public string Name { get; }
		public string DisplayName { get; }
		public object Value { get; set; }
	}
}
