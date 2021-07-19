using Emmock.Application;
using Emmock.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Emmock.Desktop
{
	public class PageParameterHistory
	{
		public string ParamName { get; set; }
		public object Value { get; set; }
	}

	public class WpfPageService : IPageService
	{
		private readonly IPageFactory m_pageFactory;
		private readonly Stack<IPageViewModel> m_pageHistoryStack = new Stack<IPageViewModel>();
		private readonly Dictionary<IPageViewModel, List<PageParameterHistory>> m_pageParameterHistory = new Dictionary<IPageViewModel, List<PageParameterHistory>>();

		public WpfPageService(IPageFactory pageFactory)
		{
			m_pageFactory = pageFactory;

			IPageViewModel page = m_pageFactory.GetPage<HomeViewModel>();
			CurrentPage = page;
		}

		public event EventHandler CurrentPageChanged;

		public IPageViewModel CurrentPage
		{
			get => m_pageHistoryStack.Count > 0 ? m_pageHistoryStack.Peek() : null;
			set => m_pageHistoryStack.Push(value);
		}

		public Stack<IPageViewModel> PageHistory => m_pageHistoryStack;

		public void ShowPage<T>(bool showOnTop, Dictionary<string, object> pageParameterBundle) where T : IPageViewModel
		{
			IPageViewModel pageToShow = m_pageFactory.GetPage<T>();
			ShowPage(pageToShow, showOnTop, pageParameterBundle);
		}

		public void ShowPage(Type pageType, bool showOnTop, Dictionary<string, object> pageParameterBundle)
		{
			IPageViewModel pageToShow = GetPageByType(pageType);
			ShowPage(pageToShow, showOnTop, pageParameterBundle);
		}

		private IPageViewModel GetPageByType(Type pageType)
		{
			return m_pageFactory.GetPage(pageType);
		}

		public void CloseCurrentPage()
		{
			if (m_pageHistoryStack.Count <= 1)
				return;

			IPageViewModel pageBeingLeft = m_pageHistoryStack.Pop();
			pageBeingLeft.Leaving();
			pageBeingLeft.Closing();

			CurrentPage.Initialize(GetParameterDictionary(CurrentPage));
			CurrentPage.Opening();
			CurrentPageChanged?.Invoke(this, null);
		}

		private void LeaveCurrentPage()
		{
			if (m_pageHistoryStack.Count < 1)
				return;

			IPageViewModel pageBeingLeft = m_pageHistoryStack.Peek();
			pageBeingLeft.Leaving();
		}

		private void ShowPage(IPageViewModel page, bool showOnTop, Dictionary<string, object> pageParameterBundle)
		{
			if (page is null)
				return;

			if (showOnTop)
				LeaveCurrentPage();
			else
				CloseCurrentPage();

			if (page.IsRootPage)
				m_pageHistoryStack.Clear();

			CurrentPage = page;

			UpdatePageParameterHistory(CurrentPage, pageParameterBundle);

			CurrentPage.Initialize(GetParameterDictionary(CurrentPage));
			CurrentPage.Opening();

			CurrentPageChanged?.Invoke(this, null);
		}

		private void UpdatePageParameterHistory(IPageViewModel page, Dictionary<string, object> pageParams)
		{
			if (pageParams is null)
				return;

			List<PageParameterHistory> pageParameters = new List<PageParameterHistory>();
			foreach (var entry in pageParams)
			{
				PageParameterHistory pageParameter = new PageParameterHistory()
				{
					ParamName = entry.Key,
					Value = entry.Value
				};

				pageParameters.Add(pageParameter);
			}

			m_pageParameterHistory[page] = pageParameters;
		}

		private Dictionary<string, object> GetParameterDictionary(IPageViewModel page)
		{
			if (!m_pageParameterHistory.ContainsKey(page))
				return new Dictionary<string, object>();

			List<PageParameterHistory> paramHistory = m_pageParameterHistory[page];
			return paramHistory.ToDictionary(x => x.ParamName, x => x.Value);
		}
	}
}
