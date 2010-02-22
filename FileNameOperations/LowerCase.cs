using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for LowerCase.
	/// </summary>
	[Serializable()]
	public class LowerCase : Operation
	{
		public LowerCase()
		{
		}

		public override string Name
		{
			get{ return "lower case";}
		}

		public override string Group
		{
			get{ return "Case";}
		}

		public override string Help
		{
			get{ return "Change all characters to small characters.";}
		}

		public override Shortcut Key
		{
			get{ return Shortcut.CtrlL;}
		}

		public override void Perform(ListViewItem lvi)
		{
			FileName f = (FileName)lvi.Tag;

			f.NewName = f.NewName.ToLower();
		}
	}
}
