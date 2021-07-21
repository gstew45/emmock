using Emmock.Core.Interfaces.Services;
using Emmock.Core.Models;
using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Emmock.Application.ViewModels
{
	public class EquipmentDetailsViewModel : BasePageViewModel
	{
		private readonly IEquipmentService m_equipmentService;

		public EquipmentDetailsViewModel(IEquipmentService equipmentService, IPageService pageService) : base(pageService)
		{
			m_equipmentService = equipmentService;

			CreateNewEquipmentCommand = new DelegateCommand(CreateNewEquipment);
			ShowEquipmentDetailsCommand = new DelegateCommand<Equipment>(ShowEquipmentDetails);
		}

		public ICommand CreateNewEquipmentCommand { get; }
		public ICommand ShowEquipmentDetailsCommand { get; }

		public ObservableCollection<Equipment> Equipment { get; } = new ObservableCollection<Equipment>();

		public Equipment EquipmentDetails { get; private set; }
		public bool IsEquipmentUnderEdit { get; set; } = false;

		public override string Title => $"{EquipmentDetails.Name} Details";

		public override string Image => "";

		public override bool IsRootPage => false;

		public void Initialize(string equipmentId)
		{
			EquipmentDetails = m_equipmentService.GetEquipmentById(equipmentId);

			// Get Rigs equipment by rig id.
			Equipment.Clear();
			foreach (Equipment equipment in m_equipmentService.GetEquipmentByParentId(EquipmentDetails.Id))
			{
				Equipment.Add(equipment);
			}
		}

		public void EditEquipment()
		{
			IsEquipmentUnderEdit = true;
		}

		public void ConfirmEdit()
		{
			m_equipmentService.UpdateEquipment(EquipmentDetails);
			IsEquipmentUnderEdit = false;
		}

		public void CancelEdit()
		{
			EquipmentDetails = m_equipmentService.GetEquipmentById(EquipmentDetails.Id);
			IsEquipmentUnderEdit = false;
		}

		public override void Initialize(Dictionary<string, object> pageParameterBundle)
		{
			string equipmentId = pageParameterBundle["EquipmentId"] as string;
			if (string.IsNullOrEmpty(equipmentId))
				return;

			Initialize(equipmentId);
		}

		private void ShowEquipmentDetails(Equipment equipment)
		{
			if (equipment == null)
				return;

			Dictionary<string, object> pageParams = new Dictionary<string, object>();
			pageParams.Add("EquipmentId", equipment.Id);

			m_pageService.ShowPage<EquipmentDetailsViewModel>(true, pageParams);
		}

		private void CreateNewEquipment()
		{
			Dictionary<string, object> pageParams = new Dictionary<string, object>();
			pageParams.Add("EquipmentDetails", EquipmentDetails);

			m_pageService.ShowPage<CreateNewEquipmentViewModel>(true, pageParams);
		}
	}
}
