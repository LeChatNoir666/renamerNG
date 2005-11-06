using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for InsertNumber.
	/// </summary>
	[Serializable()]
	public class InsertNumber : Operation
	{
		[NonSerialized()]int number;

		public InsertNumber()
		{
			this[0] = "{0}";
			this[1] = "1";
			this[2] = "1";
			this[3] = "0,false";
		}

		public override string Name
		{
			get { return "Insert number";}
		}

		public override string Group
		{
			get { return "";}
		}

		public override string Help
		{
			get { return "Insert a number.";}
		}

		public override int ParameterCount
		{
			get { return 4;}
		}

		public override Shortcut Key
		{
			get { return Shortcut.None;}
		}

		public override GUIElement[] Gui
		{
			get
			{
				return new GUIElement[]
				{
					new GUIElement(GUIElement.Types.TEXTBOX, "Format", "", "Format of the number to insert, example: {0:d2} for 01, 02, 03 ..."),
					new GUIElement(GUIElement.Types.NUMERICUPDOWN, "Start value", "-2000000000,2000000000", "Start value for first number to be inserted"),
					new GUIElement(GUIElement.Types.NUMERICUPDOWN, "Change value", "-2000000000,2000000000", "Value to add for each file, for example 1 to increase by one."),
					new GUIElement(GUIElement.Types.POSITION, "Position", "", "Select position to insert number at")
				};
			}
		}

		/// <summary>
		/// Override Init because this operation needs to
		/// initialize the number value that holds the current
		/// number to insert.
		/// </summary>
		public override void Init()
		{
			number = int.Parse(this[1]);
		}

		public override void Perform(ListViewItem lvi)
		{
			FileName f = (FileName)lvi.Tag;

			string text = String.Format(this[0], number);
			number += int.Parse(this[2]);
			string[] s = this[3].Split(',');
			int start = int.Parse(s[0]);
			bool fromEnd = bool.Parse(s[1]);

			string res = "", data = f.NewName;

			if (!fromEnd)
			{
				if (start > data.Length)
					res = data.Insert(data.Length, text);
				else
					res = data.Insert(start, text);
			}
			else
			{
				if (start > data.Length)
					res = data.Insert(0, text);
				else
					res = data.Insert(data.Length - start, text);
			}

			f.NewName = res;
		}
	}
}
