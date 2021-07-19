using Emmock.Application.Supporting;
using Emmock.Core.Interfaces.Services;
using Prism.Commands;
using System.Collections.Generic;
using System.Windows.Input;

namespace Emmock.Application.ViewModels
{
	public class CreateSystemViewModel : ObservableObject, IPageViewModel
	{
		private readonly IEquipmentService m_equipmentService;
		private readonly IPageService m_pageService;

		private string m_rigId;

		public CreateSystemViewModel(IEquipmentService equipmentService, IPageService pageService)
		{
			m_equipmentService = equipmentService;
			m_pageService = pageService;

			CreateSystemCommand = new DelegateCommand(CreateSystem);
			CancelCommand = new DelegateCommand(Cancel);
		}

		public ICommand CreateSystemCommand { get; }
		public ICommand CancelCommand { get; }

		public string Name { get; set; }
		public string Type { get; set; }

		public bool IsRootPage => false;

		public string Title => "Create New System";

		public string Image => "";


		public void Closing()
		{
		}

		public void Initialize(Dictionary<string, object> pageParameterBundle)
		{
			string rigId = pageParameterBundle["RigId"] as string;
			if (string.IsNullOrEmpty(rigId))
				return;

			m_rigId = rigId;
		}

		public void Leaving()
		{
		}

		public void Opening()
		{
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