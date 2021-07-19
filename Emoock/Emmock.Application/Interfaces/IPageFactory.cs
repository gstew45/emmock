using System;

namespace Emmock.Application
{
	public interface IPageFactory
	{
		IPageViewModel GetPage<T>() where T : IPageViewModel;
		IPageViewModel GetPage(Type pageType);
	}
}
