using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for InvertCase.
	/// </summary>
	[Serializable()]
	public class InvertCase : Operation
	{
		public InvertCase()
		{
		}

		public override string Name
		{
			get{ return "iNVERT CASE";}
		}

		public override string Group
		{
			get{ return "Case";}
		}

		public override string Help
		{
			get{ return "Change lower case letters to capital letters and capital letters to lower case letters.";}
		}

		public override void Perform(ListViewItem lvi)
		{
			FileName f = (FileName)lvi.Tag;

			if (f.NewName.Length == 0) return;

			System.Text.StringBuilder s = new System.Text.StringBuilder(f.NewName);
			for (int i = 0 ; i < s.Length ; i++)
				if (char.IsLower(s[i]))
					s[i] = char.ToUpper(s[i]);
				else
					s[i] = char.ToLower(s[i]);

			f.NewName = s.ToString();
		}
	}
}
