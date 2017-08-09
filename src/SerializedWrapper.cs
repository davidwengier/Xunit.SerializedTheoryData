using System;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace Xunit
{
	internal abstract class SerializedWrapper
	{
		public abstract object WrappedValue { get; }
	}

	internal class SerializedWrapper<T> : SerializedWrapper, IXunitSerializable
	{
		private const string ValueField = "Value";
		private T m_value;

		public override object WrappedValue
		{
			get { return m_value; }
		}

		public SerializedWrapper()
		{
		}

		public SerializedWrapper(T value)
		{
			this.m_value = value;
		}

		public void Deserialize(IXunitSerializationInfo info)
		{
			m_value = JsonConvert.DeserializeObject<T>(info.GetValue<string>(ValueField));
		}

		public void Serialize(IXunitSerializationInfo info)
		{
			info.AddValue(ValueField, JsonConvert.SerializeObject(m_value), typeof(string));
		}
	}
}