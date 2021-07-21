using BlazorFluentUI;
using Emmock.Application.ViewModels;
using Emmock.Core.Models;
using Microsoft.AspNetCore.Components;

namespace Emmock.WebUI.Pages
{
	public partial class Rigs
	{
		[Inject]
		public RigsViewModel RigsViewModel { get; set; }

		[Inject]
		public NavigationManager Navigation { get; set; }

		public void ViewRig(Rig rig)
		{
			Navigation.NavigateTo($"/rig/details?rigId={rig.Id}");
		}

		protected override void OnInitialized()
		{
			base.OnInitialized();

			RigsViewModel.Initialize(null);
			StateHasChanged();
		}
	}
}
