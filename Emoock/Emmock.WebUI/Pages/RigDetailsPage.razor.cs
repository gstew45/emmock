using Emmock.Application.ViewModels;
using Emmock.Core.Models;
using Microsoft.AspNetCore.Components;

namespace Emmock.WebUI.Pages
{
	public partial class RigDetailsPage
	{
		[Inject]
		public RigDetailsViewModel DetailsViewModel { get; set; }

		[Inject]
		public NavigationManager Navigation { get; set; }

		public void ViewEquipment(Equipment equipment)
		{
			Navigation.NavigateTo($"/equipment/details?equipmentId={equipment.Id}");
		}

		protected override void OnInitialized()
		{
			base.OnInitialized();
			Navigation.TryGetQueryString<string>("rigId", out string rigId);

			DetailsViewModel.Initialize(rigId);
			StateHasChanged();
		}
	}
}
