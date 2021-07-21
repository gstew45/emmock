using Emmock.Application.Models;
using Emmock.Application.Supporting;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Emmock.Application.ViewModels
{
	public class MainViewModel : ObservableObject
	{
		private readonly IPageService m_pageService;
		private readonly ISideBarContextFactory m_sideBarContextFactory;
		private readonly BreadcrumbNavigationViewModel m_breadcrumbNavigationViewModel;

		public MainViewModel(IPageService pageService, ISideBarContextFactory sideBarContextFactory, BreadcrumbNavigationViewModel breadcrumbNavigationViewModel)
		{
			m_pageService = pageService;
			m_pageService.CurrentPageChanged += OnCurrentPageChanged;

			m_sideBarContextFactory = sideBarContextFactory;
			m_breadcrumbNavigationViewModel = breadcrumbNavigationViewModel;

			SelectPageCommand = new DelegateCommand<Type>(SelectPage);
		}

		public ICommand SelectPageCommand { get; }

		public IPageViewModel CurrentPage => m_pageService.CurrentPage;

		public BreadcrumbNavigationViewModel BreadcrumbViewModel => m_breadcrumbNavigationViewModel;

		public ObservableCollection<SideBarItem> SideBarItems { get; } = new ObservableCollection<SideBarItem>();

		public void Initialize()
		{
			m_pageService.Initialize();
			SelectPageCommand.Execute(CurrentPage.GetType());
		}

		private void SelectPage(Type pageTypeToSelect)
		{
			m_pageService.ShowPage(pageTypeToSelect, showOnTop: false, null);
		}

		private void OnCurrentPageChanged(object sender, EventArgs e)
		{
			OnPropertyChanged(nameof(CurrentPage));

			ResetSideBar();
		}

		private void ResetSideBar()
		{
			SideBarItems.Clear();
			foreach (SideBarItem sideBarItem in m_sideBarContextFactory.GetSideBarItemsInContext(CurrentPage.GetType()))
			{
				SideBarItems.Add(sideBarItem);
			}
		}
	}
}
