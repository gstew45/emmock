using Emmock.Application.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Emmock.WebUI.Pages
{
	public partial class CreateNewRig
	{
		[Inject]
		public CreateNewRigViewModel CreateRigViewModel { get; set; }

		[Inject]
		public NavigationManager Navigation { get; set; }

		protected override void OnInitialized()
		{
			base.OnInitialized();

			CreateRigViewModel.RigCreated += CreateRigViewModel_RigCreated;
		}

		private void CreateRigViewModel_RigCreated(object sender, System.EventArgs e)
		{
			CreateRigViewModel.RigCreated -= CreateRigViewModel_RigCreated;
			Navigation.NavigateTo("/rigs");
		}
	}
}
