using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for SentenceCase.
	/// </summary>
	[Serializable()]
	public class SentenceCase : Operation
	{
		public SentenceCase()
		{
		}

		public override string Name
		{
			get{ return "Sentence case";}
		}

		public override string Group
		{
			get{ return "Case";}
		}

		public override string Help
		{
			get{ return "First letter is large, the rest are small.";}
		}

		public override void Perform(ListViewItem lvi)
		{
			FileName f = (FileName)lvi.Tag;

			if (f.NewName.Length == 0) return;

			if (f.NewName.Length == 1)
			{
				f.NewName = f.NewName.ToUpper();
				return;
			}

			f.NewName = f.NewName.Substring(0, 1).ToUpper() + f.NewName.Substring(1).ToLower();
		}
	}
}
