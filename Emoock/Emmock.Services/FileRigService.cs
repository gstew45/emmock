using Emmock.Core.Interfaces;
using Emmock.Core.Interfaces.Services;
using Emmock.Core.Models;
using System.Collections.Generic;
using System.Linq;

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

		public Rig CreateRig(string rigType, string name, string description) => m_rigRepo.Create(rigType, name, description);

		public Rig GenerateRig(string rigType, string name, string description) => m_rigGenerator.GenerateRig(rigType, name, description);

		public IEnumerable<Rig> GetAllRigs() => m_rigRepo.GetAll();

		public Rig GetRigById(string rigId) => GetAllRigs().FirstOrDefault(r => r.Id == rigId);

		public void UpdateRig(Rig rigUnderEdit) => m_rigRepo.Update(rigUnderEdit);
	}
}
