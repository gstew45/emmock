using Emmock.Core.Interfaces.Services;
using Emmock.Core.Models;
using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Emmock.Application.ViewModels
{
	public class RigsViewModel : IPageViewModel
	{
		private readonly IRigService m_rigService;
		private readonly IPageService m_pageService;

		public RigsViewModel(IRigService rigService, IPageService pageService)
		{
			m_rigService = rigService;
			m_pageService = pageService;

			CreateNewRigCommand = new DelegateCommand(CreateNewRig);
			ShowRigDetailsCommand = new DelegateCommand<Rig>(ShowRigDetails);
		}

		public ICommand CreateNewRigCommand { get; }
		public ICommand ShowRigDetailsCommand { get; }

		public ObservableCollection<Rig> Rigs { get; } = new ObservableCollection<Rig>();

		public string Title => "Rigs";

		public string Image => "";

		public bool IsRootPage => true;

		public void Closing()
		{
		}

		public void Initialize()
		{
			foreach (Rig rig in m_rigService.GetAllRigs())
			{
				Rigs.Add(rig);
			}
		}

		public void Initialize(Dictionary<string, object> pageParameterBundle)
		{
			Initialize();
		}

		public void Leaving()
		{
			Rigs.Clear();
		}

		public void Opening()
		{
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
