using Emmock.Application.Interfaces;
using Emmock.Application.ViewModels.Controls;
using Emmock.Core.Interfaces.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Emmock.Application.ViewModels
{
	public class CreateTestViewModel : BasePageViewModel
	{
		private readonly ITestService m_testService;

		public CreateTestViewModel(ITestService testService, 
								   IToolboxViewModel toolboxViewModel, 
								   IFormSurfaceViewModel formSurfaceViewModel,
								   IPropertiesViewModel propertiesViewModel, 
								   IPageService pageService) 
			: base(pageService)
		{
			m_testService = testService;
			ToolboxViewModel = toolboxViewModel;
			FormSurfaceViewModel = formSurfaceViewModel;
			PropertiesViewModel = propertiesViewModel;

			ConfirmCommand = new DelegateCommand(Confirm);
			CancelCommand = new DelegateCommand(Cancel);
		}

		public ICommand ConfirmCommand { get; }
		public ICommand CancelCommand { get; }

		public override string Title => "Create Test";

		public override string Image => "";

		public override bool IsRootPage => false;

		public string TestName { get; set; }
		public string TestDescription { get; set; }
		public DateTime TestDueDate { get; set; }

		public IToolboxViewModel ToolboxViewModel { get; }
		public IFormSurfaceViewModel FormSurfaceViewModel { get; }
		public IPropertiesViewModel PropertiesViewModel { get; }

		public override void Initialize(Dictionary<string, object> pageParameterBundle)
		{
			base.Initialize(pageParameterBundle);

			ToolboxViewModel.Initialize();
			PropertiesViewModel.Initialize();
		}

		private void Cancel()
		{
			m_pageService.CloseCurrentPage();
		}

		private void Confirm()
		{
			m_testService.CreateTest(TestName, TestDescription, TestDueDate);

			m_pageService.CloseCurrentPage();
		}
	}
}