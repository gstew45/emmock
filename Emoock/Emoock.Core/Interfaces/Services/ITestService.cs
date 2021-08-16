using Emmock.Core.Models;
using System;
using System.Collections.Generic;

namespace Emmock.Core.Interfaces.Services
{
	public interface ITestService
	{
		Test CreateTest(string name, string description, DateTime dueDate);
		IEnumerable<Test> GetAllTests();
		Test GetTestById(string testId);
		IEnumerable<Test> GetUpcomingTests(int numberOfTests);
		void UpdateTest(Test testToUpdate);
	}
}
