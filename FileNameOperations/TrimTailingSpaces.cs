using System;
using System.Windows.Forms;
using RenamerNG;
using System.Text.RegularExpressions;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for TrimTailingSpaces.
	/// </summary>
	[Serializable()]
	public class TrimTailingSpaces : Operation
	{
		public TrimTailingSpaces()
		{
		}

		public override string Name
		{
			get{ return "Trim tailing spaces";}
		}

		public override string Group
		{
			get{ return "Trim";}
		}

		public override string Help
		{
			get{ return "Removes any occurance of spaces at the end.";}
		}

		public override void Perform(ListViewItem lvi)
		{
			FileName f = (FileName)lvi.Tag;

			try
			{
				f.NewName = Regex.Replace(f.NewName, @"\s*\Z", "", RegexOptions.Singleline);
			}
			catch
			{
				f.Success = false;
			}
		}
	}
}
