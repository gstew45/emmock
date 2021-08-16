using Emmock.Core.Interfaces;
using Emmock.Core.Interfaces.Services;
using Emmock.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Emmock.Services
{
	public class FileTestService : ITestService
	{
		private readonly ITestRepository m_testRepo;

		public FileTestService(ITestRepository rigRepo)
		{
			m_testRepo = rigRepo;
		}

		public Test CreateTest(string name, string description, DateTime dueDate) => m_testRepo.Create(name, description, dueDate);

		public IEnumerable<Test> GetAllTests() => m_testRepo.GetAll();

		public Test GetTestById(string testId) => m_testRepo.GetAll().FirstOrDefault(t => t.Id == testId);

		public IEnumerable<Test> GetUpcomingTests(int numberOfTests)
		{
			IEnumerable<Test> tests = GetAllTests().Where(t => t.Status != TestStatus.Executed);
			return tests.OrderBy(t => t.DueDate).Take(numberOfTests);
		}

		public void UpdateTest(Test testToUpdate) => m_testRepo.Update(testToUpdate);
	}
}
