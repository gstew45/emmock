using Emmock.Core.Interfaces.Services;
using System;

namespace Emmock.Application.ViewModels
{
	public class CreateNewRigViewModel
	{
		private readonly IRigService m_rigService;

		public CreateNewRigViewModel(IRigService rigService)
		{
			m_rigService = rigService;
		}

		public event EventHandler RigCreated;

		public string Name { get; set; }
		public string Type { get; set; }

		public void CreateRig()
		{
			m_rigService.GenerateRig(Type);
			RigCreated?.Invoke(this, null);
		}
	}
}
