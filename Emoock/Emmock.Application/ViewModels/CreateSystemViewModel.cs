using Emmock.Core.Interfaces.Services;
using Prism.Commands;
using System.Collections.Generic;
using System.Windows.Input;

namespace Emmock.Application.ViewModels
{
	public class CreateSystemViewModel : BasePageViewModel
	{
		private readonly IEquipmentService m_equipmentService;

		private string m_rigId;

		public CreateSystemViewModel(IEquipmentService equipmentService, IPageService pageService) : base(pageService)
		{
			m_equipmentService = equipmentService;

			CreateSystemCommand = new DelegateCommand(CreateSystem);
			CancelCommand = new DelegateCommand(Cancel);
		}

		public ICommand CreateSystemCommand { get; }
		public ICommand CancelCommand { get; }

		public string Name { get; set; }
		public string Type { get; set; }

		public override bool IsRootPage => false;

		public override string Title => "Create New System";

		public override string Image => "";

		public override void Initialize(Dictionary<string, object> pageParameterBundle)
		{
			string rigId = pageParameterBundle["RigId"] as string;
			if (string.IsNullOrEmpty(rigId))
				return;

			m_rigId = rigId;
		}

		private void CreateSystem()
		{
			m_equipmentService.CreateEquipment(m_rigId, null, Name, Type, isSystem: true);

			m_pageService.CloseCurrentPage();
		}

		private void Cancel()
		{
			m_pageService.CloseCurrentPage();
		}
	}
}