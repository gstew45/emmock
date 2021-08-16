using Emmock.Core;
using Emmock.Core.Interfaces;
using Emmock.Core.Supporting;
using System.Collections.ObjectModel;

namespace Emmock.Application.ViewModels
{
	public class EditFieldViewModel : ObservableObject
	{
		private readonly ObservableCollection<string> m_types = new ObservableCollection<string>()
		{
			"DateTime",
			"Double",
			"Text",
			"Number"
		};

		private string m_type;

		public EditFieldViewModel()
		{

		}

		public ObservableCollection<string> Types => m_types;

		public string Name { get; set; }

		public string Type 
		{
			get => m_type;

			set
			{
				m_type = value;
				Value = string.Empty;
			}
		}

		public string Value { get; set; }

		public IField GetField()
		{
			FieldFactory fieldFactory = new FieldFactory();
			return fieldFactory.CreateField(Type, Name, Value);
		}
	}
}
