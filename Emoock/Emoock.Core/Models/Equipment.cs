using System;
using System.Collections.Generic;

namespace Emmock.Core.Models
{
	public interface IField
	{
		string Name { get; set; }
		string Type { get; }
		object GetValue();
		void SetValue(object value);
	}

	public abstract class Field<T> : IField
	{
		public string Name { get; set; }
		public T TypedValue { get; set; }

		public abstract string Type { get; }

		public object GetValue()
		{
			return TypedValue;
		}

		public void SetValue(object value)
		{
			TypedValue = (T)value;
		}
	}

	public class DateTimeField : Field<DateTime>
	{
		public override string Type => "DateTime";
	}

	public class Equipment
	{
		public string Id { get; set; }
		public string RigId { get; set; }
		public List<IField> Fields { get; } = new List<IField>();
	}
}
