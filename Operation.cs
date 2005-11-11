using System;
using System.Windows.Forms;

namespace RenamerNG
{
	/// <summary>
	/// Summary description for Operation.
	/// </summary>
	[Serializable()]
	public abstract class Operation : IComparable
	{
		string[] parameters;

		abstract public string Name
		{
			get;
		}

		abstract public string Group
		{
			get;
		}

		abstract public string Help
		{
			get;
		}

		abstract public int ParameterCount
		{
			get;
		}

		abstract public Shortcut Key
		{
			get;
		}

		abstract public GUIElement[] Gui
		{
			get;
		}

		/// <summary>
		/// Is the operation recordable?
		/// Default is true.
		/// Some special operations (like FreeEdit) are not recordable.
		/// </summary>
		public virtual bool Recordable
		{
			get { return true; }
		}

		/// <summary>
		/// Does the operation require work mode selected?
		/// Default is false.
		/// Most list operations requires work mode selected.
		/// </summary>
		public virtual bool RequiresWorkModeSelected
		{
			get { return false; }
		}

		/// <summary>
		/// Does the operation affect all items?
		/// Default is false, meaning it works on the items specified by the work mode.
		/// Some list operations (like Select All) ignore the work mode and work on all items.
		/// </summary>
		public virtual bool AffectAllItems
		{
			get { return false; }
		}

		public string this[int index]
		{
			get
			{
				return parameters[index];
			}

			set
			{
				parameters[index] = value;
			}
		}

		public Operation()
		{
			parameters = new string[ParameterCount];
		}

		/// <summary>
		/// Initialize operation, most operations don't need this
		/// so a default method is provided (does nothing).
		/// </summary>
		public virtual void Init()
		{
		}

		public void SetParameters(string[] data)
		{
			for (int i = 0 ; i < data.Length ; i++)
				this[i] = data[i];
		}

		public int CompareTo(object obj)
		{
			return Name.CompareTo(obj.ToString());
		}

		public override string ToString()
		{
			string s = Name;

			for (int i = 0 ; i < ParameterCount ; i++)
			{
				string p = this[i];
				p = p.Replace("\\", "\\\\");
				p = p.Replace("\"", "\\\"");
				p = p.Replace("|", "\\|");
				p = p.Replace("\n", "\\n");
				s += '|' + p;
			}

			return s;
		}

		abstract public void Perform(ListViewItem lvi);
	}
}
