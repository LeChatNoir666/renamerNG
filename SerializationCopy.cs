using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace RenamerNG
{
	/// <summary>
	/// Copies an object by performing serialization and deserialization.
	/// </summary>
	public class SerializationCopy
	{
		private SerializationCopy()
		{
		}

		public static object Copy(object o)
		{
			MemoryStream ms = new MemoryStream();
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(ms, o);
			ms.Position = 0;
			return bf.Deserialize(ms);
		}
	}
}
