using System;
using System.Windows.Forms;
using RenamerNG;
using System.Text.RegularExpressions;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for RegExp.
	/// </summary>
	[Serializable()]
	public class RegExp : Operation
	{
		public RegExp()
		{
			this[2] = "false";
		}

		public override string Name
		{
			get{ return "RegExp";}
		}

		public override string Group
		{
			get{ return "";}
		}

		public override string Help
		{
			get{ return "Applies a regular expression to the filename.";}
		}

		public override int ParameterCount
		{
			get{ return 3;}
		}

		public override GUIElement[] Gui
		{
			get
			{
				return new GUIElement[]
				{
					new GUIElement(GUIElement.Types.TEXTBOX, "Pattern", "", "The format to match"),
					new GUIElement(GUIElement.Types.TEXTBOX, "Replacement", "ALLOWEMPTY", "The new format"),
					new GUIElement(GUIElement.Types.CHECKBOX, "Match case", "", "Match case")
				};
			}
		}

		public override void Perform(ListViewItem lvi)
		{
			FileName f = (FileName)lvi.Tag;

			string pattern = this[0];
			string replacement = this[1];
			bool matchCase = bool.Parse(this[2]);

			try
			{
                RegexOptions opt = RegexOptions.Singleline;
                if (!matchCase) opt |= RegexOptions.IgnoreCase;

                if (Regex.Match(f.NewName, pattern, opt).Success)
                    f.NewName = Regex.Replace(f.NewName, pattern, replacement, opt);
			}
			catch
			{
				f.Success = false;
			}
		}
	}
}
