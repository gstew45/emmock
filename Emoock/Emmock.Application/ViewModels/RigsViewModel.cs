using Emmock.Core.Interfaces.Services;
using Emmock.Core.Models;
using System.Collections.Generic;

namespace Emmock.Application.ViewModels
{
	public class RigsViewModel
	{
		private readonly IRigService m_rigService;

		public RigsViewModel(IRigService rigService)
		{
			m_rigService = rigService;
		}

		public IEnumerable<Rig> Rigs { get; private set; }

		public void Initialize()
		{
			Rigs = m_rigService.GetAllRigs();
		}
	}
}
