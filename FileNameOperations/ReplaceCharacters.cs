using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for ReplaceCharacters.
	/// </summary>
	[Serializable()]
	public class ReplaceCharacters : Operation
	{
		public ReplaceCharacters()
		{
			this[2] = "false";
		}

		public override string Name
		{
			get{ return "Replace characters";}
		}

		public override string Group
		{
			get{ return "";}
		}

		public override string Help
		{
			get{ return "Search for the occurence of characters and replace it with other characters.";}
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
					new GUIElement(GUIElement.Types.TEXTBOX, "Replace", "", "Enter the characters to find, each character must have a corresponding character in the 'with' string."),
					new GUIElement(GUIElement.Types.TEXTBOX, "With", "", "Enter the characters to replace with, each character must have a corresponding character in the 'replace' string."),
					new GUIElement(GUIElement.Types.CHECKBOX, "Match case", "", "Match case")
				};
			}
		}

		public override void Perform(ListViewItem lvi)
		{
			FileName f = (FileName)lvi.Tag;

			string replace = this[0];
			string with = this[1];

			if (replace.Length != with.Length) throw new ArgumentException("'replace' and 'with' strings must have the same length.");

			bool matchCase = bool.Parse(this[2]);

			string data = f.NewName;

			if (matchCase)
			{
				for (int i = 0 ; i < replace.Length ; i++)
				{
					data = data.Replace(replace[i], with[i]);
				}
			}
			else
			{
				for (int i = 0 ; i < replace.Length ; i++)
				{
					data = Replace.ReplaceCaseInsensitive(data, replace[i].ToString(), with[i].ToString());
				}
			}

			f.NewName = data;
		}
	}
}
