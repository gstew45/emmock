using Emmock.Core.Interfaces.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Emmock.Application.ViewModels
{
	public class CreateNewRigViewModel : IPageViewModel
	{
		private readonly IRigService m_rigService;
		private readonly IPageService m_pageService;

		public CreateNewRigViewModel(IRigService rigService, IPageService pageService)
		{
			m_rigService = rigService;
			m_pageService = pageService;
			CreateRigCommand = new DelegateCommand(CreateRig);
			CancelCommand = new DelegateCommand(Cancel);
		}

		public event EventHandler RigCreated;

		public ICommand CreateRigCommand { get; }
		public ICommand CancelCommand { get; }

		public string Name { get; set; }
		public string Type { get; set; }
		public string Description { get; set; }

		public string Title => "Create New Rig";

		public string Image => "";

		public bool IsRootPage => false;

		public void CreateRig()
		{
			m_rigService.GenerateRig(Type, Name, Description);
			RigCreated?.Invoke(this, null);

			m_pageService.CloseCurrentPage();
		}

		private void Cancel()
		{
			m_pageService.CloseCurrentPage();
		}

		public void Initialize(Dictionary<string, object> pageParameterBundle)
		{
		}

		public void Leaving()
		{
			Name = string.Empty;
			Type = string.Empty;
		}

		public void Opening()
		{
		}

		public void Closing()
		{
		}
	}
}
