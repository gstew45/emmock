using Emmock.Core.Interfaces.Services;
using Emmock.Core.Models;
using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Emmock.Application.ViewModels
{
	public class RigDetailsViewModel : BasePageViewModel
	{
		private readonly IRigService m_rigService;
		private readonly IEquipmentService m_equipmentService;

		public RigDetailsViewModel(IRigService rigService, IEquipmentService equipmentService, IPageService pageService) : base(pageService)
		{
			m_rigService = rigService;
			m_equipmentService = equipmentService;

			EditRigCommand = new DelegateCommand(EditRig);
			CreateNewSystemCommand = new DelegateCommand(CreateSystem);
			ShowSystemDetailsCommand = new DelegateCommand<Equipment>(ShowSystemDetails);
		}

        public ICommand EditRigCommand { get; }
		public ICommand CreateNewSystemCommand { get; }
		public ICommand ShowSystemDetailsCommand { get; }

		public Rig RigDetails { get; private set; }

		public ObservableCollection<Equipment> Systems { get; } = new ObservableCollection<Equipment>();

		public override string Title => $"{RigDetails.Name} Details";

		public override string Image => "";

		public override bool IsRootPage => false;

		public void Initialize(string rigId)
		{
			// Get Rig By Id
			RigDetails = m_rigService.GetRigById(rigId);

			// Get Rigs equipment by rig id.
			Systems.Clear();
			foreach (Equipment system in m_equipmentService.GetEquipmentByRigId(rigId).Where(e => e.IsSystem))
			{
				Systems.Add(system);
			}
		}

		public override void Initialize(Dictionary<string, object> pageParameterBundle)
		{
			string rigId = pageParameterBundle["RigId"] as string;
			if (string.IsNullOrEmpty(rigId))
				return;

			Initialize(rigId);
		}

		private void EditRig()
		{
			Dictionary<string, object> pageParameters = new Dictionary<string, object>();
			pageParameters["RigId"] = RigDetails.Id;

			m_pageService.ShowPage<EditRigViewModel>(true, pageParameters);
		}

		private void CreateSystem()
		{
			Dictionary<string, object> pageParameters = new Dictionary<string, object>();
			pageParameters["RigId"] = RigDetails.Id;

			m_pageService.ShowPage<CreateSystemViewModel>(true, pageParameters);
		}

		private void ShowSystemDetails(Equipment system)
		{
			if (system == null)
				return;

			Dictionary<string, object> pageParams = new Dictionary<string, object>();
			pageParams.Add("EquipmentId", system.Id);

			m_pageService.ShowPage<EquipmentDetailsViewModel>(true, pageParams);
		}
	}
}
