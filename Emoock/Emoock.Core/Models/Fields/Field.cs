using Emmock.Core.Interfaces;

namespace Emmock.Core.Models
{
	public abstract class Field<T> : IField
	{
		public string Name { get; set; }
		public T TypedValue { get; set; }

		public abstract string Type { get; }

		public object GetValue()
		{
			return TypedValue;
		}

		public void SetValue(object value)
		{
			TypedValue = (T)value;
		}
	}
}
