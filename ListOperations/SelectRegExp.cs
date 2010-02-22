using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace RenamerNG.ListOperations
{
  /// <summary>
  /// Summary description for SelectMatching.
  /// </summary>
  [Serializable()]
  public class SelectRegExp : Operation
  {
    public SelectRegExp()
    {
      this[1] = "false";
    }

    public override string Name
    {
      get{ return "Select Matching RegExp";}
    }

    public override string Group
    {
      get{ return "Selection";}
    }

    public override string Help
    {
      get{ return "Selects all items in the list that matches a regular expression.";}
    }

    public override int ParameterCount
    {
      get{ return 2;}
    }

    public override GUIElement[] Gui
    {
      get
      {
        return new GUIElement[]
        {
          new GUIElement(GUIElement.Types.TEXTBOX, "Match", "", "The regular expression to match"),
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
        FileName f = (FileName)lvi.Tag;

        string pattern = this[0];
        bool matchCase = bool.Parse(this[1]);

        try
        {
            Match m;
            
            if (matchCase)
                m = Regex.Match(f.NewName, pattern, RegexOptions.Singleline);
            else
                m = Regex.Match(f.NewName, pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            lvi.Selected = m.Success;
        }
        catch
        {
            f.Success = false;
        }
    }
  }
}
