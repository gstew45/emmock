using System.Collections.Generic;

namespace Emmock.Application
{
	public interface IPageViewModel
	{
		bool IsRootPage { get; }
		string Title { get; }
		string Image { get; }
		void Initialize(Dictionary<string, object> pageParameterBundle);
		void Opening();
		void Leaving();
		void Closing();
	}
}
