using System;
using System.Windows.Forms;

namespace RenamerNG.ListOperations
{
	/// <summary>
	/// Summary description for SelectNone.
	/// </summary>
	[Serializable()]
	public class SelectNone : Operation
	{
		public SelectNone()
		{
		}

		public override string Name
		{
			get{ return "Select None";}
		}

		public override string Group
		{
			get{ return "Selection";}
		}

		public override string Help
		{
			get{ return "Deselects all items in the list.";}
		}

		public override int ParameterCount
		{
			get{ return 0;}
		}

		public override Shortcut Key
		{
			get{ return Shortcut.CtrlN;}
		}

		public override GUIElement[] Gui
		{
			get
			{
				return null;
			}
		}
		
		public override bool RequiresWorkModeSelected
		{
			get { return true; }
		}

		public override void Perform(ListViewItem lvi)
		{
			lvi.Selected = false;
		}
	}
}
