using System.Collections.Generic;

namespace Emmock.Core.Models
{
	public class RigTemplate
	{
		public List<EquipmentTemplate> EquipmentTemplates { get; } = new List<EquipmentTemplate>();
	}

	public class EquipmentTemplate
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public List<IField> Fields { get; } = new List<IField>();
	}
}