using System;
using System.Windows.Forms;

namespace RenamerNG.ListOperations
{
  /// <summary>
  /// Summary description for InvertSelection.
  /// </summary>
  [Serializable()]
  public class InvertSelection : Operation
  {
    public InvertSelection()
    {
    }

    public override string Name
    {
      get{ return "Invert Selection";}
    }

    public override string Group
    {
      get{ return "Selection";}
    }

    public override string Help
    {
      get{ return "Inverts selection status on all items in the list.";}
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
      lvi.Selected = !lvi.Selected;
    }
  }
}
