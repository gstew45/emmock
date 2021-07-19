using Emmock.Core.Interfaces;
using Emmock.Core.Models;
using Microsoft.AspNetCore.Components;
using System;

namespace Emmock.WebUI.Components
{
	public partial class FieldComponent
	{
		public IFieldComponentFactory FieldComponentFactory { get; set; } = new FieldComponentFactory();

		[Parameter]
		public IField Field { get; set; }

		public Type FieldComponentType { get; set; }

		protected override void OnInitialized()
		{
			FieldComponentType = FieldComponentFactory.GetFieldComponentType(Field);
			base.OnInitialized();
		}
	}
}
