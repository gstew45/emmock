using Emmock.Application.Supporting;
using System;

namespace Emmock.Application.Models
{
	public class SideBarItem : ObservableObject
	{
		public string Text { get; set; }
		public string Image { get; set; }
		public Type AssociatedPageType { get; set; }
	}
}
