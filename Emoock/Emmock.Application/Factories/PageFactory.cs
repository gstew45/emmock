using System;

namespace Emmock.Application.Factories
{
	public class PageFactory : IPageFactory
	{
		private readonly IServiceProvider m_serviceProvider;

		public PageFactory(IServiceProvider serviceProvider)
		{
			m_serviceProvider = serviceProvider;
		}

		public IPageViewModel GetPage<T>() where T : IPageViewModel
		{
			Type pageType = typeof(T);
			return (IPageViewModel)m_serviceProvider.GetService(pageType);
		}

		public IPageViewModel GetPage(Type pageType)
		{
			if (pageType.IsAssignableTo(typeof(IPageViewModel)))
				return (IPageViewModel)m_serviceProvider.GetService(pageType);

			return null;
		}
	}
}
