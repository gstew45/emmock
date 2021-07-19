using Emmock.Application.Supporting;
using System.Collections.Generic;

namespace Emmock.Application.ViewModels
{
	public class HomeViewModel : ObservableObject, IPageViewModel
	{
		public HomeViewModel()
		{
		}

		public string Title => "Home";

		public string Image => "";

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
