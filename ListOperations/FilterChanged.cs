using System;
using System.Windows.Forms;

namespace RenamerNG.ListOperations
{
  /// <summary>
  /// Summary description for FilterChanged.
  /// </summary>
  [Serializable()]
  public class FilterChanged : Operation
  {
    public FilterChanged()
    {
    }

    public override string Name
    {
      get{ return "Filter changed";}
    }

    public override string Group
    {
      get{ return "Filter";}
    }

    public override string Help
    {
      get{ return "Removes all items that are not changed.";}
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
      if (!f.Changed)
        lvi.Remove();
    }
  }
}
