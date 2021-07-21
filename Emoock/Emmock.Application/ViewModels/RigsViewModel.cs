using Emmock.Core.Interfaces.Services;
using Emmock.Core.Models;
using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Emmock.Application.ViewModels
{
	public class RigsViewModel : BasePageViewModel
	{
		private readonly IRigService m_rigService;

		public RigsViewModel(IRigService rigService, IPageService pageService) : base(pageService)
		{
			m_rigService = rigService;

			CreateNewRigCommand = new DelegateCommand(CreateNewRig);
			ShowRigDetailsCommand = new DelegateCommand<Rig>(ShowRigDetails);
		}

		public ICommand CreateNewRigCommand { get; }
		public ICommand ShowRigDetailsCommand { get; }

		public ObservableCollection<Rig> Rigs { get; } = new ObservableCollection<Rig>();

		public override string Title => "Rigs";

		public override string Image => "";

		public override bool IsRootPage => true;

		public override void Initialize(Dictionary<string, object> pageParameterBundle)
		{
			foreach (Rig rig in m_rigService.GetAllRigs())
			{
				Rigs.Add(rig);
			}
		}

		public override void Leaving()
		{
			Rigs.Clear();
		}

		private void CreateNewRig()
		{
			m_pageService.ShowPage<CreateNewRigViewModel>(true, null);
		}

		private void ShowRigDetails(Rig rig)
		{
			if (rig == null)
				return;

			Dictionary<string, object> pageParams = new Dictionary<string, object>();
			pageParams.Add("RigId", rig.Id);

			m_pageService.ShowPage<RigDetailsViewModel>(true, pageParams);
		}
	}
}
