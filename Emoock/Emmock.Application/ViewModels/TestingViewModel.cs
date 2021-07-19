
using System.Collections.Generic;

namespace Emmock.Application.ViewModels
{
	public class TestingViewModel : IPageViewModel
	{
		public string Title => "Testing";

		public string Image => throw new System.NotImplementedException();

		public bool IsRootPage => true;

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
