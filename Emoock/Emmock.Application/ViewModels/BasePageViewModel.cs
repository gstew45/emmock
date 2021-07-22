using Emmock.Core.Supporting;
using System.Collections.Generic;

namespace Emmock.Application.ViewModels
{
	public abstract class BasePageViewModel : ObservableObject, IPageViewModel
	{
		protected readonly IPageService m_pageService;

		public BasePageViewModel(IPageService pageService)
		{
			m_pageService = pageService;
		}

		public abstract string Title { get; }

		public abstract string Image { get; }

		public abstract bool IsRootPage { get; }

		public virtual void Closing()
		{
		}

		public virtual void Initialize(Dictionary<string, object> pageParameterBundle)
		{
		}

		public virtual void Leaving()
		{
		}

		public virtual void Opening()
		{
		}
	}
}
