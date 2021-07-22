using Emmock.Core.Interfaces;
using System;

namespace Emmock.Core.Models
{
	public class DateTimeField : Field<DateTime>
	{
		public override string Type => "DateTime";

		public override IField Copy()
		{
			DateTimeField copy = new DateTimeField();

			copy.Name = Name;
			copy.TypedValue = TypedValue;

			return copy;
		}
	}
}
