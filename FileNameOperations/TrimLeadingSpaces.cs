using System;
using System.Windows.Forms;
using RenamerNG;
using System.Text.RegularExpressions;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for TrimLeadingSpaces.
	/// </summary>
	[Serializable()]
	public class TrimLeadingSpaces : Operation
	{
		public TrimLeadingSpaces()
		{
		}

		public override string Name
		{
			get{ return "Trim leading spaces";}
		}

		public override string Group
		{
			get{ return "";}
		}

		public override string Help
		{
			get{ return "Removes any occurance of spaces at the beginning.";}
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

			try
			{
				f.NewName = Regex.Replace(f.NewName, @"\A\s*", "", RegexOptions.Singleline);
			}
			catch
			{
				f.Success = false;
			}
		}
	}
}
