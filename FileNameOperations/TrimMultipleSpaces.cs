using System;
using System.Windows.Forms;
using RenamerNG;
using System.Text.RegularExpressions;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for TrimMultipleSpaces.
	/// </summary>
	[Serializable()]
	public class TrimMultipleSpaces : Operation
	{
		public TrimMultipleSpaces()
		{
		}

		public override string Name
		{
			get{ return "Trim multiple spaces";}
		}

		public override string Group
		{
			get{ return "";}
		}

		public override string Help
		{
			get{ return "Removes occurances of multiple spaces and replace them with a single space.";}
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
				f.NewName = Regex.Replace(f.NewName, @"\s{2,}", " ", RegexOptions.Singleline);
			}
			catch
			{
				f.Success = false;
			}
		}
	}
}
