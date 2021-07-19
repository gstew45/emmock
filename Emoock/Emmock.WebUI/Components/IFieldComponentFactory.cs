using Emmock.Core.Interfaces;
using Emmock.Core.Models;
using System;

namespace Emmock.WebUI.Components
{
	public interface IFieldComponentFactory
	{
		Type GetFieldComponentType(IField field);
	}

	public class FieldComponentFactory : IFieldComponentFactory
	{
		public Type GetFieldComponentType(IField field)
		{
			if (field is DateTimeField)
				return typeof(DateTimeFieldComponent);

			return null;
		}
	}
}