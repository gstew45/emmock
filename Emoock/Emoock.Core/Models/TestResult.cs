using System;

namespace Emmock.Core.Models
{
	public class TestResult
	{
		public string Id { get; set; }
		public string TestId { get; set; }
		public DateTime ExecutedDateTime { get; set; }
		public bool HasPassed { get; set; }
	}
}
