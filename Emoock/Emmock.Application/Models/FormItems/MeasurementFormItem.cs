using Emmock.Application.Interfaces;
using Emmock.Application.Models.Properties;
using Emmock.Core.Supporting;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Emmock.Application.Models.FormItems
{
	public class MeasurementFormItem : ObservableObject, IFormItem
	{
		private string m_name = string.Empty;
		private bool m_isSelected = false;

		public MeasurementFormItem()
		{
			Properties.Add(new PropertyValue("Expected", "Expected"));
			Properties.Add(new PropertyValue("Actual", "Actual"));
			Properties.Add(new PickListPropertyValue("Unit", "Unit", new List<string>() { "psi", "cm", "inches", "mm" }));
			Properties.Add(new BooleanPropertyValue("IsRequired", "Required"));
		}

		public ObservableCollection<PropertyValue> Properties { get; } = new ObservableCollection<PropertyValue>();

		public string Name
		{
			get => m_name;
			set
			{
				m_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		public bool IsSelected
		{
			get => m_isSelected;
			set
			{
				m_isSelected = value;
				OnPropertyChanged(nameof(IsSelected));
			}
		}
	}
}
