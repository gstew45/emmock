using System;

namespace Emmock.Application.ViewModels
{
	public class RecentTestsViewModel : BasePageViewModel
	{
		public RecentTestsViewModel(IPageService pageService) : base(pageService)
		{
		}

		public override string Title => "Recent tests";

		public override string Image => throw new NotImplementedException();

		public override bool IsRootPage => false;
	}
}
