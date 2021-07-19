using Emmock.Application.Models;
using System;
using System.Collections.Generic;

namespace Emmock.Application
{
	public interface ISideBarContextFactory
	{
		IEnumerable<SideBarItem> GetSideBarItemsInContext(Type pageType);
	}
}
