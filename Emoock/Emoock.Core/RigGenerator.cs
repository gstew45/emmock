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

		public Rig GenerateRig(string rigType, string name, string description)
		{
			RigTemplate rigTemplate = m_rigTemplateRepository.GetRigTemplate(rigType);

			Rig rig = m_rigRepository.Create(rigType, name, description);

			foreach (EquipmentTemplate equipmentTemplate in rigTemplate.EquipmentTemplates)
			{
				Equipment currentEquipment = m_equipmentRepository.Create(rig.Id, null, equipmentTemplate.Name, equipmentTemplate.Type, equipmentTemplate.IsSystem);
				currentEquipment.Fields.AddRange(equipmentTemplate.Fields);

				foreach (EquipmentTemplate subTemplate in equipmentTemplate.SubTemplates)
				{
					// All sub items must set isSystem to false.
					Equipment subEquipment = m_equipmentRepository.Create(rig.Id, currentEquipment.Id, subTemplate.Name, subTemplate.Type, isSystem: false);
					subEquipment.Fields.AddRange(subTemplate.Fields);

					m_equipmentRepository.Update(subEquipment);
				}

				m_equipmentRepository.Update(currentEquipment);
			}

			return rig;
		}
	}
}
