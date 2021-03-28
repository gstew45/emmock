using Emmock.Application.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Emmock.WebUI.Pages
{
	public partial class Rigs
	{
		[Inject]
		public RigsViewModel RigsViewModel { get; set; }

		protected override void OnInitialized()
		{
			base.OnInitialized();

			RigsViewModel.Initialize();
		}
	}
}
