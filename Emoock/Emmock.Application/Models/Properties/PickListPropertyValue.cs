using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Emmock.Application.Models.Properties
{

	public class PickListPropertyValue : PropertyValue
	{
		public PickListPropertyValue(string name, string displayName, List<string> pickList) : base(name, displayName)
		{
			PickList = new ObservableCollection<string>(pickList);
		}

		public ObservableCollection<string> PickList { get; }
	}
}