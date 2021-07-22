using Emmock.Core.Interfaces;

namespace Emmock.Core.Models
{
	public class NumberField : Field<int>
	{
		public override string Type => "Number";

		public override IField Copy()
		{
			NumberField copy = new NumberField();

			copy.Name = Name;
			copy.TypedValue = TypedValue;

			return copy;
		}
	}
}
