using System;
using System.Collections;
using RenamerNG.FileNameOperations;

namespace RenamerNG.Macros
{
	/// <summary>
	/// Summary description for Macro.
	/// </summary>
	[Serializable()]
	public class Macro : IEnumerable, IEnumerator
	{
		private string name;
		private string description;
		private ArrayList operations;

		public Macro(string name)
		{
			this.name = name;
			this.description = "";
			operations = new ArrayList(16);
		}
/*
		public Macro(Macro m)
		{
			this.name = m.Name;
			operations = new ArrayList(16);
			foreach (Operation o in m)
				operations.Add(SerializationCopy.Copy(o));
		}
*/
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		public void AddOperation(Operation op)
		{
			operations.Add(op);
		}

		public Operation this[int index]
		{
			get
			{
				return (Operation)operations[index];
			}

			set
			{
				operations[index] = value;
			}
		}

		public override string ToString()
		{
			return name;
		}

		private int position = -1;

		public IEnumerator GetEnumerator()
		{
			Reset();

			return (IEnumerator)this;
		}

		#region IEnumerator Members

		public void Reset()
		{
			position = -1;
		}

		public object Current
		{
			get
			{
				return operations[position];
			}
		}

		public bool MoveNext()
		{
			if (position < operations.Count - 1)
			{
				position++;
				return true;
			}

			return false;
		}

		#endregion
	}
}
