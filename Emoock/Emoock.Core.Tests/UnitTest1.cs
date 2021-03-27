using Emmock.Persistance.Serializers;
using Emoock.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Emoock.Core.Tests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			RigTemplate rigTemplate = new RigTemplate();

			for (int i = 0; i < 5; i++)
			{
				rigTemplate.EquipmentTemplates.Add(CreateEquipmentTemplate());
			}

			EmmockJsonSerializer serializer = new EmmockJsonSerializer();

			string json = serializer.SerializeObject(rigTemplate);
			Console.WriteLine(json);

			RigTemplate deserialzied = serializer.DeserializeObject<RigTemplate>(json);

			Assert.IsNotNull(deserialzied);
		}

		private EquipmentTemplate CreateEquipmentTemplate()
		{
			EquipmentTemplate equipmentTemplate = new EquipmentTemplate();
			equipmentTemplate.Fields.AddRange(CreateFields());

			return equipmentTemplate;
		}

		private IEnumerable<IField> CreateFields()
		{
			List<DateTimeField> fields = new List<DateTimeField>();
			for (int i = 0; i < 6; i++)
			{
				fields.Add(new DateTimeField());
			}

			return fields;
		}
	}
}
