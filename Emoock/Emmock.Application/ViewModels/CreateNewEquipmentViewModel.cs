using Emmock.Core.Interfaces.Services;
using Emmock.Core.Models;
using Prism.Commands;
using System.Collections.Generic;
using System.Windows.Input;

namespace Emmock.Application.ViewModels
{
	public class CreateNewEquipmentViewModel : BasePageViewModel
	{
		private readonly IEquipmentService m_equipmentService;

		private Equipment m_parentDetails;

		public CreateNewEquipmentViewModel(IEquipmentService equipmentService, IPageService pageService) : base(pageService)
		{
			m_equipmentService = equipmentService;

			CreateEquipmentCommand = new DelegateCommand(CreateEquipment);
			CancelCommand = new DelegateCommand(Cancel);
		}

		public ICommand CreateEquipmentCommand { get; }
		public ICommand CancelCommand { get; }

		public string Name { get; set; }
		public string Type { get; set; }

		public override bool IsRootPage => false;

		public override string Title => "Create New Equipment";

		public override string Image => "";

		public override void Initialize(Dictionary<string, object> pageParameterBundle)
		{
			Equipment parentDetails = pageParameterBundle["EquipmentDetails"] as Equipment;
			if (parentDetails == null)
				return;

			m_parentDetails = parentDetails;
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