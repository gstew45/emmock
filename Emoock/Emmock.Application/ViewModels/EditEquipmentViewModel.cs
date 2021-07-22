using Emmock.Core.Interfaces.Services;
using Emmock.Core.Models;
using Prism.Commands;
using System.Collections.Generic;
using System.Windows.Input;

namespace Emmock.Application.ViewModels
{
	public class EditEquipmentViewModel : BasePageViewModel
	{
		private IEquipmentService m_equipmentService;
		private Equipment m_equipmentUnderEdit;

		public EditEquipmentViewModel(IEquipmentService equipmentService, IPageService pageService) : base(pageService)
		{
			m_equipmentService = equipmentService;

			ConfirmCommand = new DelegateCommand(Confirm);
			CancelCommand = new DelegateCommand(Cancel);
		}

		public ICommand ConfirmCommand { get; }
		public ICommand CancelCommand { get; }

		public override string Title => "Edit Equipment";

		public override string Image => "";

		public override bool IsRootPage => false;

		public Equipment EquipmentUnderEdit => m_equipmentUnderEdit;

		public override void Initialize(Dictionary<string, object> pageParameterBundle)
		{
			string equipmentId = pageParameterBundle["EquipmentId"] as string;
			if (string.IsNullOrEmpty(equipmentId))
				return;

			Equipment equipmentToEdit = m_equipmentService.GetEquipmentById(equipmentId);
			m_equipmentUnderEdit = equipmentToEdit.Copy();
		}

		public void Confirm()
		{
			m_equipmentService.UpdateEquipment(EquipmentUnderEdit);

			m_pageService.CloseCurrentPage();
		}

		private void Cancel()
		{
			m_pageService.CloseCurrentPage();
		}
	}
}