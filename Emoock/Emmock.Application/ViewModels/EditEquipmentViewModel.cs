using Emmock.Core.Interfaces;
using Emmock.Core.Interfaces.Services;
using Emmock.Core.Models;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

		public ObservableCollection<EditFieldViewModel> Fields { get; } = new ObservableCollection<EditFieldViewModel>();

		public override void Initialize(Dictionary<string, object> pageParameterBundle)
		{
			string equipmentId = pageParameterBundle["EquipmentId"] as string;
			if (string.IsNullOrEmpty(equipmentId))
				return;

			Equipment equipmentToEdit = m_equipmentService.GetEquipmentById(equipmentId);
			m_equipmentUnderEdit = equipmentToEdit.Copy();

			Fields.Clear();
			foreach (IField field in m_equipmentUnderEdit.Fields)
			{
				EditFieldViewModel editFieldViewModel = new EditFieldViewModel()
				{
					Name = field.Name,
					Type = field.Type,
					Value = Convert.ToString(field.GetValue())
				};

				Fields.Add(editFieldViewModel);
			}
		}

		public void Confirm()
		{
			m_equipmentUnderEdit.Fields.Clear();
			m_equipmentUnderEdit.Fields.AddRange(Fields.Select(f => f.GetField()));

			m_equipmentService.UpdateEquipment(EquipmentUnderEdit);

			m_pageService.CloseCurrentPage();
		}

		private void Cancel()
		{
			m_pageService.CloseCurrentPage();
		}
	}
}