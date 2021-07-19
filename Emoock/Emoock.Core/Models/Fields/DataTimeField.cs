using System;

namespace Emmock.Core.Models
{
	public class DateTimeField : Field<DateTime>
	{
		public override string Type => "DateTime";
	}
}
