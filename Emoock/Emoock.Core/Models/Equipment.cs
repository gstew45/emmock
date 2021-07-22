using Emmock.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Emmock.Core.Models
{
	public class Equipment : ICopy<Equipment>
	{
		public string Id { get; set; }
		public string RigId { get; set; }
		public string ParentEquipmentId { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public bool IsSystem { get; set; }
		public List<IField> Fields { get; } = new List<IField>();
		public List<Equipment> ChildEquipment { get; } = new List<Equipment>();

		public Equipment Copy()
		{
			Equipment copy = new Equipment();

			copy.Id = Id;
			copy.RigId = RigId;
			copy.ParentEquipmentId = ParentEquipmentId;
			copy.Name = Name;
			copy.Type = Type;
			copy.IsSystem = IsSystem;

			copy.Fields.AddRange(Fields.Select(f => f.Copy()));
			copy.ChildEquipment.AddRange(ChildEquipment.Select(f => f.Copy()));

			return copy;
		}
	}
}
