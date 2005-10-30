using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for Undo.
	/// </summary>
	[Serializable()]
	public class Undo : Operation
	{
		public Undo()
		{
		}

		public override string Name
		{
			get{ return "Undo";}
		}

		public override string Group
		{
			get{ return "";}
		}

		public override string Help
		{
			get{ return "Undo changes performed by last operation.";}
		}

		public override int ParameterCount
		{
			get{ return 0;}
		}

		public override Shortcut Key
		{
			get{ return Shortcut.CtrlZ;}
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

			f.Undo();
		}
	}
}
