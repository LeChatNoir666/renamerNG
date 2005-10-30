using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for UpperCase.
	/// </summary>
	[Serializable()]
	public class UpperCase : Operation
	{
		public UpperCase()
		{
		}

		public override string Name
		{
			get{ return "UPPER CASE";}
		}

		public override string Group
		{
			get{ return "Case";}
		}

		public override string Help
		{
			get{ return "Change all characters to large characters.";}
		}

		public override int ParameterCount
		{
			get{ return 0;}
		}

		public override Shortcut Key
		{
			get{ return Shortcut.CtrlU;}
		}

		public override GUIElement[] Gui
		{
			get
			{
				return null;
			}
		}

		public override void Perform(ListViewItem lvi)
		{
			FileName f = (FileName)lvi.Tag;

			f.NewName = f.NewName.ToUpper();
		}
	}
}
