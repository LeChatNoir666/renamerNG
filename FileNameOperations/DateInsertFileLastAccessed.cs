using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for DateInsertFileLastAccessed.
	/// </summary>
	[Serializable()]
	public class DateInsertFileLastAccessed : DateInsertCurrent
	{
		public override string Name
		{
			get { return "Insert file last accessed date";}
		}

		public override string Help
		{
			get { return "Insert the date and time of when the file was last accessed.";}
		}

		public override void Perform(ListViewItem lvi)
		{
			FileName f = (FileName)lvi.Tag;
			DateTime date = f.LastAccess;
			Perform(lvi, date);
		}
	}
}
