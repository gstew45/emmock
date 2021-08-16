using Emmock.Core.Models;
using System;
using System.Collections.Generic;

namespace Emmock.Core.Interfaces
{
	public interface ITestRepository
	{
		Test Create(string name, string description, DateTime dueDate);
		IEnumerable<Test> GetAll();
		void Update(Test rigUnderEdit);
	}
}
