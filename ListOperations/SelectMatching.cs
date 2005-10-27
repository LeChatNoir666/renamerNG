using System;
using System.Windows.Forms;

namespace RenamerNG.ListOperations
{
	/// <summary>
	/// Summary description for SelectMatching.
	/// </summary>
	[Serializable()]
	public class SelectMatching : Operation
	{
		public SelectMatching()
		{
			this[1] = "false";
		}

		public override string Name
		{
			get{ return "Select Matching";}
		}

		public override string Group
		{
			get{ return "Selection";}
		}

		public override string Help
		{
			get{ return "Selects all items in the list that matches a certain string.";}
		}

		public override int ParameterCount
		{
			get{ return 2;}
		}

		public override Shortcut Key
		{
			get{ return Shortcut.None;}
		}

		public override GUIElement[] Gui
		{
			get
			{
				return new GUIElement[]
				{
					new GUIElement(GUIElement.Types.TEXTBOX, "Match", "", "The text to match"),
					new GUIElement(GUIElement.Types.CHECKBOX, "Match case", "", "Match case")
				};
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
			FileName f = ((FileName)lvi.Tag);
			bool matchCase = bool.Parse(this[1]);

			if (matchCase)
				lvi.Selected = f.NewName.IndexOf(this[0]) >= 0;
			else
				lvi.Selected = f.NewName.ToLower().IndexOf(this[0].ToLower()) >= 0;
		}
	}
}
