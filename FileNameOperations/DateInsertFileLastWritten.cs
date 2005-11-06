using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for DateInsertFileLastWritten.
	/// </summary>
	[Serializable()]
	public class DateInsertFileLastWritten : DateInsertCurrent
	{
		public override string Name
		{
			get { return "Insert file last written date";}
		}

		public override string Help
		{
			get { return "Insert the date and time of when the file was last written.";}
		}

		public override void Perform(ListViewItem lvi)
		{
			FileName f = (FileName)lvi.Tag;
			DateTime date = f.LastWrite;
			Perform(lvi, date);
		}
	}
}
