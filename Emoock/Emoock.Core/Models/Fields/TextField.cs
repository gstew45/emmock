using Emmock.Core.Interfaces;

namespace Emmock.Core.Models
{
	public class TextField : Field<string>
	{
		public override string Type => "Text";

		public override IField Copy()
		{
			TextField copy = new TextField();

			copy.Name = Name;
			copy.TypedValue = TypedValue;

			return copy;
		}
	}
}
