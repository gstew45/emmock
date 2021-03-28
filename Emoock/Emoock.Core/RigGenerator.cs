using Emmock.Core.Interfaces;
using Emmock.Core.Models;

namespace Emmock.Core
{
	public sealed class RigGenerator : IRigGenerator
	{
		private readonly IRigRepository m_rigRepository;
		private readonly IEquipmentRepository m_equipmentRepository;
		private readonly IRigTemplateRepository m_rigTemplateRepository;

		public RigGenerator(IRigRepository rigRepository, IEquipmentRepository equipmentRepository, IRigTemplateRepository rigTemplateRepository)
		{
			m_rigRepository = rigRepository;
			m_equipmentRepository = equipmentRepository;
			m_rigTemplateRepository = rigTemplateRepository;
		}

		public Rig GenerateRig(string rigType)
		{
			RigTemplate rigTemplate = m_rigTemplateRepository.GetRigTemplate(rigType);

			Rig rig = m_rigRepository.Create(rigType);

			foreach (EquipmentTemplate equipmentTemplate in rigTemplate.EquipmentTemplates)
			{
				Equipment currentEquipment = m_equipmentRepository.Create(rig.Id, equipmentTemplate.Name, equipmentTemplate.Type);

				currentEquipment.Fields.AddRange(equipmentTemplate.Fields);
				m_equipmentRepository.Update(currentEquipment);
			}

			return rig;
		}
	}
}
