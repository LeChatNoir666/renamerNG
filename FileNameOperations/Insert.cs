using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.FileNameOperations
{
  /// <summary>
  /// Summary description for Insert.
  /// </summary>
  [Serializable()]
  public class Insert : Operation
  {
    public Insert()
    {
      this[1] = "0,false";
    }

    public override string Name
    {
      get { return "Insert";}
    }

    public override string Group
    {
      get { return "";}
    }

    public override string Help
    {
      get { return "Insert text.";}
    }

    public override int ParameterCount
    {
      get { return 2;}
    }

    public override Shortcut Key
    {
      get { return Shortcut.CtrlI;}
    }

    public override GUIElement[] Gui
    {
      get
      {
        return new GUIElement[]
        {
          new GUIElement(GUIElement.Types.TEXTBOX, "Insert", "", "The text to insert"),
          new GUIElement(GUIElement.Types.POSITION, "Position", "", "Select position to insert text at")
        };
      }
    }

    public override void Perform(ListViewItem lvi)
    {
      FileName f = (FileName)lvi.Tag;

      string text = this[0];
      string[] s = this[1].Split(',');
      int start = int.Parse(s[0]);
      bool fromEnd = bool.Parse(s[1]);

      string res = "", data = f.NewName;

      if (!fromEnd)
      {
        if (start > data.Length)
          res = data.Insert(data.Length, text);
        else
          res = data.Insert(start, text);
      }
      else
      {
        if (start > data.Length)
          res = data.Insert(0, text);
        else
          res = data.Insert(data.Length - start, text);
      }

      f.NewName = res;
    }
  }
}
