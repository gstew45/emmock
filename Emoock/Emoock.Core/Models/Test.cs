using Emmock.Core.Supporting;
using System;

namespace Emmock.Core.Models
{
	public enum TestOccurence
	{
		OneOff,
		Daily,
		Weekly,
		Monthly,
		Yearly
	}

	public class TestSchedule
	{
		public string Id { get; set; }
		public string TestTemplateId { get; set; }
		public TestOccurence Occurence { get; set; }
		public DateTime ScheduledDate { get; set; }
	}

	public enum TestStatus
	{
		NotStarted,
		InProgress,
		Executed
	}

	public class Test : ObservableObject
	{
		public string Id { get; set; }
		public string Description { get; set; }
		public string Name { get; set; }
		public DateTime DueDate { get; set; }
		public TestStatus Status { get; set; }

		/// <summary>
		///		Here for now but would prefer to handle selection and UI related properties within Emmock.Application level.
		/// </summary>
		private bool m_isSelected = false;
		public bool IsSelected
		{
			get => m_isSelected;
			set
			{
				m_isSelected = value;
				OnPropertyChanged(nameof(IsSelected));
			}
		}
	}
}
