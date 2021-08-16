namespace Emmock.Application.Models.Properties
{
	public class BooleanPropertyValue : PropertyValue
	{
		public BooleanPropertyValue(string name, string displayName) : base(name, displayName)
		{
			Value = false;
		}
	}
}
