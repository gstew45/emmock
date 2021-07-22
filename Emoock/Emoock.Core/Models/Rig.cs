using Emmock.Core.Interfaces;
using Emmock.Core.Supporting;

namespace Emmock.Core.Models
{
	public class Rig : ObservableObject, ICopy<Rig>
	{
		public string Id { get; set; }
		public string Type { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public Rig Copy()
		{
			Rig copy = new Rig();

			copy.Id = Id;
			copy.Type = Type;
			copy.Name = Name;
			copy.Description = Description;

			return copy;
		}
	}
}
