using System;
using System.Collections.Generic;

namespace Emmock.Application.ViewModels
{
	public abstract class BasePageViewModel : IPageViewModel
	{
		public string Title => throw new NotImplementedException();

		public string Image => throw new NotImplementedException();

		public bool IsRootPage => throw new NotImplementedException();

		public void Closing()
		{
			throw new NotImplementedException();
		}

		public virtual void Initialize(Dictionary<string, object> pageParameterBundle)
		{
			throw new NotImplementedException();
		}

		public void Leaving()
		{
			throw new NotImplementedException();
		}

		public void Opening()
		{
			throw new NotImplementedException();
		}
	}
}
