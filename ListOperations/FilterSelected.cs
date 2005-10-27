using System;
using System.Windows.Forms;

namespace RenamerNG.ListOperations
{
	/// <summary>
	/// Summary description for FilterSelected.
	/// </summary>
	[Serializable()]
	public class FilterSelected : Operation
	{
		public FilterSelected()
		{
		}

		public override string Name
		{
			get{ return "Filter selected";}
		}

		public override string Group
		{
			get{ return "Filter";}
		}

		public override string Help
		{
			get{ return "Removes all items that are not selected.";}
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
			if (!lvi.Selected)
				lvi.Remove();
		}
	}
}
