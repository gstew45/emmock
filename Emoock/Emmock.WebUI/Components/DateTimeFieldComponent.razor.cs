using Emmock.Core.Interfaces;
using Emmock.Core.Models;
using Microsoft.AspNetCore.Components;
using System;

namespace Emmock.WebUI.Components
{
	public partial class DateTimeFieldComponent
	{
		[Parameter]
		public IField Field { get; set; }

		public DateTime? Value
		{
			get => (DateTime)Field.GetValue();
			set
			{
				if (value is DateTime)
					Field.SetValue(value);
			}
		}
	}
}
