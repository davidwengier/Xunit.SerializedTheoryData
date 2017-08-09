using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace Xunit
{
	internal class SerializedList<T> : List<T>, IXunitSerializable
	{
		private const string CountField = "Count";
		private const string ItemTypeFieldPrefix = "ItemType";
		private const string ItemFieldPrefix = "Item";

		public SerializedList()
		{
		}

		public SerializedList(List<T> values)
		{
			this.AddRange(values);
		}

		public void Deserialize(IXunitSerializationInfo info)
		{
			int count = info.GetValue<int>(CountField);
			this.Capacity = count;
			for (int i = 0; i < count; i++)
			{
				string typeName = info.GetValue<string>(ItemTypeFieldPrefix + i);
				Type type = Type.GetType(typeName);
				this.Add((T)JsonConvert.DeserializeObject(info.GetValue<string>(ItemFieldPrefix + i), type));
			}
		}

		public void Serialize(IXunitSerializationInfo info)
		{
			info.AddValue(CountField, this.Count, typeof(int));
			for (int i = 0; i < this.Count; i++)
			{
				info.AddValue(ItemTypeFieldPrefix + i, this[i].GetType().AssemblyQualifiedName, typeof(string));
				info.AddValue(ItemFieldPrefix + i, JsonConvert.SerializeObject(this[i]), typeof(string));
			}
		}
	}
}