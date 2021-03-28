using Emmock.Core;
using Emmock.Core.Interfaces;
using Emmock.Core.Interfaces.Services;
using Emmock.Core.Models;
using System.Collections.Generic;

namespace Emmock.Services
{
	public class FileRigService : IRigService
	{
		private readonly IRigRepository m_rigRepo;
		private readonly IRigGenerator m_rigGenerator;

		public FileRigService(IRigRepository rigRepo, IRigGenerator rigGenerator)
		{
			m_rigRepo = rigRepo;
			m_rigGenerator = rigGenerator;
		}

		public Rig CreateRig(string rigType) => m_rigRepo.Create(rigType);

		public Rig GenerateRig(string rigType) => m_rigGenerator.GenerateRig(rigType);

		public IEnumerable<Rig> GetAllRigs() => m_rigRepo.GetAll();
	}
}
