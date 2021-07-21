using Emmock.Core.Interfaces.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Emmock.Application.ViewModels
{
	public class CreateNewRigViewModel : BasePageViewModel
	{
		private readonly IRigService m_rigService;

		public CreateNewRigViewModel(IRigService rigService, IPageService pageService) : base(pageService)
		{
			m_rigService = rigService;

			CreateRigCommand = new DelegateCommand(CreateRig);
			CancelCommand = new DelegateCommand(Cancel);
		}

		public event EventHandler RigCreated;

		public ICommand CreateRigCommand { get; }
		public ICommand CancelCommand { get; }

		public string Name { get; set; }
		public string Type { get; set; }
		public string Description { get; set; }

		public override string Title => "Create New Rig";

		public override string Image => "";

		public override  bool IsRootPage => false;

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

		public override void Leaving()
		{
			Name = string.Empty;
			Type = string.Empty;
		}
	}
}
