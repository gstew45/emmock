using Emmock.Core.Interfaces.Services;
using Emmock.Core.Models;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Emmock.Application.ViewModels
{
	public class TestingViewModel : BasePageViewModel
	{
		private readonly ITestService m_testService;

		public TestingViewModel(IPageService pageService, ITestService testService) : base(pageService)
		{
			m_testService = testService;

			AddTestCommand = new DelegateCommand(AddTest);
			ManageTestTemplatesCommand = new DelegateCommand(ManageTestTemplates);
			ScheduleTestCommand = new DelegateCommand(ScheduleTest);
			SelectItemCommand = new DelegateCommand<Test>(SelectItem);
			ClearSelectedItemCommand = new DelegateCommand(ClearSelectedItem);

			ViewAllScheduledTestsCommand = new DelegateCommand(ViewAllScheduledTests);
			ViewAllTestResultsCommand = new DelegateCommand(ViewAllTestResults);
		}

		public ICommand AddTestCommand { get; }
		public ICommand ManageTestTemplatesCommand { get; }
		public ICommand ScheduleTestCommand { get; }

		public ICommand ViewAllScheduledTestsCommand { get; }
		public ICommand ViewAllTestResultsCommand { get; }

		public ICommand SelectItemCommand { get; }
		public ICommand ClearSelectedItemCommand { get; }

		public override string Title => "Testing";

		public override string Image => "";

		public override bool IsRootPage => true;

		public ObservableCollection<Test> UpcomingTests { get; } = new ObservableCollection<Test>();

		public ObservableCollection<Test> RecentTestResults { get; } = new ObservableCollection<Test>();

		public Test SelectedTest => UpcomingTests.FirstOrDefault(t => t.IsSelected);

		public override void Initialize(Dictionary<string, object> pageParameterBundle)
		{
			base.Initialize(pageParameterBundle);

			UpcomingTests.Clear();
			IEnumerable<Test> upcomingTests = m_testService.GetUpcomingTests(5);
			foreach (Test test in upcomingTests)
			{
				UpcomingTests.Add(test);
			}
		}

		private void ClearSelectedItem()
		{
			SelectedTest.IsSelected = false;
			OnPropertyChanged(nameof(SelectedTest));
		}

		private void SelectItem(Test testToSelect)
		{
			if (testToSelect is null)
				return;

			foreach (Test test in UpcomingTests)
			{
				if (test == testToSelect)
					testToSelect.IsSelected = !testToSelect.IsSelected;
				else
					test.IsSelected = false;
			}

			OnPropertyChanged(nameof(SelectedTest));
		}

		private void AddTest()
		{
			m_pageService.ShowPage<CreateTestViewModel>(true, null);
		}

		private void ManageTestTemplates()
		{
			throw new NotImplementedException();
		}

		private void ScheduleTest()
		{
			throw new NotImplementedException();
		}

		private void ViewAllScheduledTests()
		{
			throw new NotImplementedException();
		}

		private void ViewAllTestResults()
		{
			throw new NotImplementedException();
		}
	}
}
