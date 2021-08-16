using Emmock.Core.Interfaces;
using Emmock.Core.Models;
using System;

namespace Emmock.Core
{
	public class FieldFactory
	{
		public IField CreateField(string type, string name, string value)
		{
			switch (type)
			{
				case "DateTime":
					return CreateDateTimeField(name, value);
				case "Number":
					return CreateNumberField(name, value);
				case "Text":
					return CreateTextField(name, value);
				case "Double":
					return CreateDoubleField(name, value);

				default:
					return CreateTextField(name, value);
			}
		}

		private DateTimeField CreateDateTimeField(string name, string value)
		{
			DateTimeField field = new DateTimeField();
			field.Name = name;
			field.SetValue(Convert.ToDateTime(value));

			return field;
		}

		private NumberField CreateNumberField(string name, string value)
		{
			NumberField field = new NumberField();
			field.Name = name;
			field.SetValue(Convert.ToInt32(value));

			return field;
		}

		private DoubleField CreateDoubleField(string name, string value)
		{
			DoubleField field = new DoubleField();
			field.Name = name;
			field.SetValue(Convert.ToDouble(value));

			return field;
		}

		private TextField CreateTextField(string name, string value)
		{
			TextField field = new TextField();
			field.Name = name;
			field.SetValue(value);

			return field;
		}
	}
}
