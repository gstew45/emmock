using Emmock.Application.ViewModels;
using Microsoft.AspNetCore.Components;

namespace Emmock.WebUI.Pages
{
	public partial class EquipmentPage
	{
		[Inject]
		public EquipmentDetailsViewModel DetailsViewModel { get; set; }

		[Inject]
		public NavigationManager Navigation { get; set; }

		protected override void OnInitialized()
		{
			base.OnInitialized();
			Navigation.TryGetQueryString<string>("equipmentId", out string equipmentId);

			DetailsViewModel.Initialize(equipmentId);
			StateHasChanged();
		}
	}
}
