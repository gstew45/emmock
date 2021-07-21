using Emmock.Application.Supporting;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Emmock.Application.ViewModels
{
	public class Breadcrumb : ObservableObject
	{
		public string Text { get; set; }
		public bool IsLastCrumb{ get; set; }
		public IPageViewModel AssociatedPage{ get; set; }
		public Dictionary<string, object> CrumbParams { get; set; }
	}

	public class BreadcrumbNavigationViewModel : ObservableObject
	{
		private readonly IPageService m_pageService;

		public BreadcrumbNavigationViewModel(IPageService pageService)
		{
			m_pageService = pageService;
			m_pageService.CurrentPageChanged += OnCurrentPageChanged;

			NavigateToCrumbCommand = new DelegateCommand<Breadcrumb>(NavigateToCrumb);
		}

		public ICommand NavigateToCrumbCommand { get; }

		public ObservableCollection<Breadcrumb> Breadcrumbs { get; } = new ObservableCollection<Breadcrumb>();

		private void UpdateBreadcrumb()
		{
			IEnumerable<IPageViewModel> pageHistory = m_pageService.PageHistory.Reverse();
			IPageViewModel lastPage = pageHistory.Last();

			Breadcrumbs.Clear();
			foreach(IPageViewModel page in pageHistory)
			{
				Breadcrumb crumb = new Breadcrumb()
				{
					Text = page.Title,
					AssociatedPage = page,
					IsLastCrumb = lastPage == page
				};

				Breadcrumbs.Add(crumb);
			}
		}

		private void NavigateToCrumb(Breadcrumb crumb)
		{
			IPageViewModel currentPage = m_pageService.CurrentPage;

			if (crumb.AssociatedPage == currentPage)
				return;

			while (crumb.AssociatedPage != currentPage)
			{
				m_pageService.CloseCurrentPage();

				currentPage = m_pageService.CurrentPage;
			}
		}

		private void OnCurrentPageChanged(object sender, EventArgs e)
		{
			UpdateBreadcrumb();
		}
	}
}
