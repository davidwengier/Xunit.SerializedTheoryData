using System;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace Xunit
{
	public class SerializedWrapper<T> : IXunitSerializable
	{
		private const string ObjectField = "Object";
		private T m_object;

		public T Object
		{
			get { return m_object; }
		}

		public SerializedWrapper()
		{
		}

		public SerializedWrapper(T obj)
		{
			this.m_object = obj;
		}

		public void Deserialize(IXunitSerializationInfo info)
		{
			m_object = JsonConvert.DeserializeObject<T>(info.GetValue<string>(ObjectField));
		}

		public void Serialize(IXunitSerializationInfo info)
		{
			info.AddValue(ObjectField, JsonConvert.SerializeObject(m_object), typeof(string));
		}

		public override string ToString()
		{
			return m_object.ToString();
		}
	}
}