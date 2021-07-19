using Emmock.Application.Models;
using Emmock.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace Emmock.Application.Services
{
	public class SideBarContextFactory : ISideBarContextFactory
	{
		private IPageFactory m_pageFactory;
		private Dictionary<Type, IEnumerable<SideBarItem>> m_sideBarItemContextMap;

		public SideBarContextFactory(IPageFactory pageFactory)
		{
			m_pageFactory = pageFactory;
			InitializeContextualPageTypeMap();
		}

		public IEnumerable<SideBarItem> GetSideBarItemsInContext(Type pageType)
		{
			if (!m_sideBarItemContextMap.TryGetValue(pageType, out IEnumerable<SideBarItem> sideBarItems))
				sideBarItems = m_sideBarItemContextMap[typeof(HomeViewModel)];

			return sideBarItems;
		}

		private void InitializeContextualPageTypeMap()
		{
			m_sideBarItemContextMap = new Dictionary<Type, IEnumerable<SideBarItem>>()
			{
				{ typeof(HomeViewModel),  GetHomeSideBarItems() },
				{ typeof(RigDetailsViewModel), GetRigDetailsSideBarItems() }
			};
		}

		private IEnumerable<SideBarItem> GetHomeSideBarItems()
		{
			List<SideBarItem> sideBarItems = new List<SideBarItem>();
			SideBarItem homeSideBarItem = new SideBarItem()
			{
				Text = "Home",
				AssociatedPageType = typeof(HomeViewModel)
			};

			SideBarItem rigsSideBarItem = new SideBarItem()
			{
				Text = "Rigs",
				AssociatedPageType = typeof(RigsViewModel)
			};

			SideBarItem testingSideBarItem = new SideBarItem()
			{
				Text = "Testing",
				AssociatedPageType = typeof(TestingViewModel)
			};

			sideBarItems.Add(homeSideBarItem);
			sideBarItems.Add(rigsSideBarItem);
			sideBarItems.Add(testingSideBarItem);

			return sideBarItems;
		}

		private IEnumerable<SideBarItem> GetRigDetailsSideBarItems()
		{
			List<SideBarItem> sideBarItems = new List<SideBarItem>();
			SideBarItem backToRigsSideBarItem = new SideBarItem()
			{
				Text = "Back to rigs",
				AssociatedPageType = typeof(RigsViewModel)
			};

			SideBarItem systemsSideBarItem = new SideBarItem()
			{
				Text = "Systems",
				AssociatedPageType = typeof(RigsViewModel)
			};

			SideBarItem recentTestsSideBarItem = new SideBarItem()
			{
				Text = "Recent tests",
				AssociatedPageType = typeof(RecentTestsViewModel)
			};

			sideBarItems.Add(backToRigsSideBarItem);
			sideBarItems.Add(systemsSideBarItem);
			sideBarItems.Add(recentTestsSideBarItem);

			return sideBarItems;
		}
	}
}
