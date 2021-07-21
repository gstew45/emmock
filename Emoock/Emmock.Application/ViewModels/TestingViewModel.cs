namespace Emmock.Application.ViewModels
{
	public class TestingViewModel : BasePageViewModel
	{
		public TestingViewModel(IPageService pageService) : base(pageService)
		{
		}

		public override string Title => "Testing";

		public override string Image => "";

		public override bool IsRootPage => true;
	}
}
