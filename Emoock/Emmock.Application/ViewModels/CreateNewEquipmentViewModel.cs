using Emmock.Application.Supporting;
using Emmock.Core.Interfaces.Services;
using Emmock.Core.Models;
using Prism.Commands;
using System.Collections.Generic;
using System.Windows.Input;

namespace Emmock.Application.ViewModels
{
	public class CreateNewEquipmentViewModel : ObservableObject, IPageViewModel
	{
		private readonly IEquipmentService m_equipmentService;
		private readonly IPageService m_pageService;

		private Equipment m_parentDetails;

		public CreateNewEquipmentViewModel(IEquipmentService equipmentService, IPageService pageService)
		{
			m_equipmentService = equipmentService;
			m_pageService = pageService;

			CreateEquipmentCommand = new DelegateCommand(CreateEquipment);
			CancelCommand = new DelegateCommand(Cancel);
		}

		public ICommand CreateEquipmentCommand { get; }
		public ICommand CancelCommand { get; }

		public string Name { get; set; }
		public string Type { get; set; }

		public bool IsRootPage => false;

		public string Title => "Create New Equipment";

		public string Image => "";


		public void Closing()
		{
		}

		public void Initialize(Dictionary<string, object> pageParameterBundle)
		{
			Equipment parentDetails = pageParameterBundle["EquipmentDetails"] as Equipment;
			if (parentDetails == null)
				return;

			m_parentDetails = parentDetails;
		}

		public void Leaving()
		{
		}

		public void Opening()
		{
		}

		private void CreateEquipment()
		{
			m_equipmentService.CreateEquipment(m_parentDetails.RigId, m_parentDetails.Id, Name, Type, isSystem: false);

			m_pageService.CloseCurrentPage();
		}

		private void Cancel()
		{
			m_pageService.CloseCurrentPage();
		}
	}
}