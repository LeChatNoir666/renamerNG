using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for Replace.
	/// </summary>
	[Serializable()]
	public class Replace : Operation
	{
		public Replace()
		{
			this[2] = "false";
		}

		public override string Name
		{
			get{ return "Replace";}
		}

		public override string Group
		{
			get{ return "";}
		}

		public override string Help
		{
			get{ return "Search for the occurence of a text and replace it with another text.";}
		}

		public override int ParameterCount
		{
			get{ return 3;}
		}

		public override Shortcut Key
		{
			get{ return Shortcut.CtrlR;}
		}

		public override GUIElement[] Gui
		{
			get
			{
				return new GUIElement[]
				{
					new GUIElement(GUIElement.Types.TEXTBOX, "Replace", "", "The text to find"),
					new GUIElement(GUIElement.Types.TEXTBOX, "With", "ALLOWEMPTY", "The text to replace with"),
					new GUIElement(GUIElement.Types.CHECKBOX, "Match case", "", "Match case")
				};
			}
		}

		/// <summary>
		/// Performs a search and replace operation on strings,
		/// the matching is not case sensitive.
		/// </summary>
		/// <param name="data">The input string</param>
		/// <param name="replace">String to find</param>
		/// <param name="with">String to insert instead of the string found</param>
		/// <returns>The resulting string</returns>
		public static string ReplaceCaseInsensitive(string data, string replace, string with)
		{
			string lower = data.ToLower();
			string match = replace.ToLower();
			int length = match.Length;

			for (int i = 0 ; i <= data.Length - length ; i++)
			{
				if (lower.Substring(i, length) == match)
				{
					data = data.Substring(0, i) + with + data.Substring(i + length);
					lower = lower.Substring(0, i) + with + lower.Substring(i + length);
					i += with.Length - 1;
				}
			}

			return data;
		}

		public override void Perform(ListViewItem lvi)
		{
			FileName f = (FileName)lvi.Tag;

			string replace = this[0];
			string with = this[1];
			bool matchCase = bool.Parse(this[2]);

			string data = f.NewName;

			if (matchCase)
			{
				data = data.Replace(replace, with);
			}
			else
			{
				data = ReplaceCaseInsensitive(data, replace, with);
			}

			f.NewName = data;
		}
	}
}
