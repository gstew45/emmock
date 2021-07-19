using Emmock.Core.Interfaces;
using System.Collections.Generic;

namespace Emmock.Core.Models
{
	public class Equipment
	{
		public string Id { get; set; }
		public string RigId { get; set; }
		public string ParentEquipmentId { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public bool IsSystem { get; set; }
		public List<IField> Fields { get; } = new List<IField>();
		public List<Equipment> ChildEquipment { get; } = new List<Equipment>();
	}
}
