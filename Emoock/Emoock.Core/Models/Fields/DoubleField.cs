using Emmock.Core.Interfaces;

namespace Emmock.Core.Models
{
	public class DoubleField : Field<double>
	{
		public override string Type => "Double";

		public override IField Copy()
		{
			DoubleField copy = new DoubleField();

			copy.Name = Name;
			copy.TypedValue = TypedValue;

			return copy;
		}
	}
}
