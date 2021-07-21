using System;
using System.Collections.Generic;

namespace Emmock.Application
{
	public interface IPageService
	{
		event EventHandler CurrentPageChanged;

		Stack<IPageViewModel> PageHistory { get; }

		IPageViewModel CurrentPage { get; set; }

		void ShowPage<T>(bool showOnTop, Dictionary<string, object> pageParameterBundle) where T : IPageViewModel;

		void ShowPage(Type pageType, bool showOnTop, Dictionary<string, object> pageParameterBundle);

		void CloseCurrentPage();
		void Initialize();
	}
}
