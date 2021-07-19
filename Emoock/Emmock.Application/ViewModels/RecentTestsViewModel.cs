using System;
using System.Collections.Generic;

namespace Emmock.Application.ViewModels
{
	public class RecentTestsViewModel : IPageViewModel
	{
		public string Title => "Recent tests";

		public string Image => throw new NotImplementedException();

		public bool IsRootPage => false;

		public void Closing()
		{
		}

		public void Initialize(Dictionary<string, object> pageParameterBundle)
		{
		}

		public void Leaving()
		{
		}

		public void Opening()
		{
		}
	}
}
