using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for TrimExtension.
	/// </summary>
	[Serializable()]
	public class TrimExtension : Operation
	{
		public TrimExtension()
		{
		}

		public override string Name
		{
			get{ return "Trim extension";}
		}

		public override string Group
		{
			get{ return "Trim";}
		}

		public override string Help
		{
			get{ return "Trims the extension from the file name (if the file has an extension)";}
		}

		public override int ParameterCount
		{
			get{ return 0;}
		}

		public override Shortcut Key
		{
			get{ return Shortcut.None;}
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

			if (f.NewName.Length == 0) return;

            int i = f.NewName.LastIndexOf('.');

            if (i > 0) f.NewName = f.NewName.Substring(0, i);
		}
	}
}
