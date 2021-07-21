namespace Emmock.Application.ViewModels
{
	public class HomeViewModel : BasePageViewModel
	{
		public HomeViewModel(IPageService pageService) : base(pageService)
		{
		}

		public override string Title => "Home";

		public override string Image => "";

		public override bool IsRootPage => true;
	}
}
