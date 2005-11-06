using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for Trim.
	/// </summary>
	[Serializable()]
	public class Trim : Operation
	{
		public Trim()
		{
			this[0] = "0,0,false";
		}

		public override string Name
		{
			get{ return "Trim characters";}
		}

		public override string Group
		{
			get{ return "Trim";}
		}

		public override string Help
		{
			get{ return "Trim away characters.";}
		}

		public override int ParameterCount
		{
			get{ return 1;}
		}

		public override Shortcut Key
		{
			get{ return Shortcut.CtrlT;}
		}

		public override GUIElement[] Gui
		{
			get
			{
				return new GUIElement[]
				{
					new GUIElement(GUIElement.Types.SELECTION, "Trim", "", "Select the characters to trim")
				};
			}
		}

		public override void Perform(ListViewItem lvi)
		{
			FileName f = (FileName)lvi.Tag;

			string[] s = this[0].Split(',');
			int start = int.Parse(s[0]);
			int length = int.Parse(s[1]);
			bool fromEnd = bool.Parse(s[2]);

			string res = "", data = f.NewName;

			if (!fromEnd)
			{
				if (start > data.Length) //String too short
					res = data;
				else
				{
					if (start + length > data.Length) //Trim the rest of the string
					{
						res = data.Substring(0, start);
					}
					else //Trim a part in the string
					{
						if (start > 0) //Trim block is not at pos 0
							res = data.Substring(0, start);
						res += data.Substring(start + length);
					}	 
				}
			}
			else
			{
				if (start > data.Length) //String too short
				{
					res = data;
				}
				else
				{
					if (start + length > data.Length) //Trim the rest of the string
					{
						res = data.Substring(data.Length - start);
					}
					else //Trim a part in the string
					{
						res = data.Substring(0, data.Length - (start + length));
						if (start > 0) //Trim block is not at pos 0
                            res += data.Substring(data.Length - start);
					}
				}
			}

			f.NewName = res;
		}
	}
}
