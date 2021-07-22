using Emmock.Core.Interfaces.Services;
using Emmock.Core.Models;
using Prism.Commands;
using System.Collections.Generic;
using System.Windows.Input;

namespace Emmock.Application.ViewModels
{
	public class EditRigViewModel : BasePageViewModel
	{
		private IRigService m_rigService;
		private Rig m_rigUnderEdit;

		public EditRigViewModel(IRigService rigService, IPageService pageService) : base(pageService)
		{
			m_rigService = rigService;

			ConfirmCommand = new DelegateCommand(Confirm);
			CancelCommand = new DelegateCommand(Cancel);
		}

		public ICommand ConfirmCommand { get; }
		public ICommand CancelCommand { get; }

		public override string Title => "Edit Rig";

		public override string Image => "";

		public override bool IsRootPage => false;

		public Rig RigUnderEdit => m_rigUnderEdit;

		public override void Initialize(Dictionary<string, object> pageParameterBundle)
		{
			string rigId = pageParameterBundle["RigId"] as string;
			if (string.IsNullOrEmpty(rigId))
				return;

			Rig rigToEdit = m_rigService.GetRigById(rigId);
			m_rigUnderEdit = rigToEdit.Copy();
		}

		public void Confirm()
		{
			m_rigService.UpdateRig(RigUnderEdit);

			m_pageService.CloseCurrentPage();
		}

		private void Cancel()
		{
			m_pageService.CloseCurrentPage();
		}
	}
}