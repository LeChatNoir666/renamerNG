using System;
using System.Windows.Forms;

namespace RenamerNG.ListOperations
{
	/// <summary>
	/// Summary description for SelectAll.
	/// </summary>
	[Serializable()]
	public class SelectAll : Operation
	{
		public SelectAll()
		{
		}

		public override string Name
		{
			get{ return "Select All";}
		}

		public override string Group
		{
			get{ return "Selection";}
		}

		public override string Help
		{
			get{ return "Selects all items in the list.";}
		}

		public override int ParameterCount
		{
			get{ return 0;}
		}

		public override Shortcut Key
		{
			get{ return Shortcut.CtrlA;}
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

		public override bool AffectAllItems
		{
			get { return true; }
		}

		public override void Perform(ListViewItem lvi)
		{
			lvi.Selected = true;
		}
	}
}
