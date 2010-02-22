using System;
using System.Windows.Forms;
using RenamerNG;
using System.Text.RegularExpressions;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for RegExpPath.
	/// </summary>
	[Serializable()]
	public class RegExpPath : Operation
	{
        public RegExpPath()
		{
			this[2] = "false";
		}

		public override string Name
		{
			get{ return "RegExp with path";}
		}

		public override string Group
		{
			get{ return "";}
		}

		public override string Help
		{
			get{ return "Applies a regular expression to the path and filename.";}
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
                string input = f.Path + f.NewName;
                RegexOptions opt = RegexOptions.Singleline;
                if (!matchCase) opt |= RegexOptions.IgnoreCase;

                if (Regex.Match(input, pattern, opt).Success)
                    f.NewName = Regex.Replace(input, pattern, replacement, opt);
			}
			catch
			{
				f.Success = false;
			}
		}
	}
}
