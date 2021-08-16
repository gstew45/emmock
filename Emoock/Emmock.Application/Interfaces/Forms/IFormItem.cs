using Emmock.Application.Models.Properties;
using Emmock.Core.Models;
using System.Collections.ObjectModel;

namespace Emmock.Application.Interfaces
{
	public interface IFormItem
	{
		ObservableCollection<PropertyValue> Properties { get; }
		ObservableCollection<Equipment> LinkedEquipment { get; }
		public string Name { get; set; }
		bool IsSelected { get; set; }
	}
}
