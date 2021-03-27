using Emmock.Persistance.Serializers;
using Emmock.Core.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Emmock.Persistance.Tests
{
	[TestClass]
	public class EmmockJsonSerializerTests
	{
		private EmmockJsonSerializer m_testSubject;

		[TestInitialize]
		public void TestInit()
		{
			m_testSubject = new EmmockJsonSerializer();
		}

		[TestMethod]
		public void EmmockJsonSerializer_Can_Be_Constructed()
		{
			Assert.IsNotNull(m_testSubject);
		}

		[TestMethod]
		public void EmmockJsonSerializer_Implements_ISerializer()
		{
			Assert.IsInstanceOfType(m_testSubject, typeof(ISerializer));
		}

		[TestMethod]
		public void SerializeObject_Serializes_Object_To_Valid_Json()
		{
			string expectedJson = "{\"Name\":\"This is a test object\",\"Value\":12}";

			string actualJson = m_testSubject.SerializeObject(new TestObject("This is a test object", 12));

			Assert.AreEqual(expectedJson, actualJson);
		}

		[TestMethod]
		public void DeserializeObject_Deserializes_Input_To_Valid_Object()
		{
			object expectedValue = DateTime.Now;
			string input = m_testSubject.SerializeObject(new TestObject("This is a test object", expectedValue));

			TestObject value = m_testSubject.DeserializeObject<TestObject>(input);

			Assert.IsNotNull(value);
			Assert.AreEqual("This is a test object", value.Name);
			Assert.AreEqual(expectedValue, value.Value);
		}

		private class TestObject
		{
			public TestObject(string name, object value)
			{
				Name = name;
				Value = value;
			}
			public string Name { get; }
			public object Value { get; }
		}
	}
}
