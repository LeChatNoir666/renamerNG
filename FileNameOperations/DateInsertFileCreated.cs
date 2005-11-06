using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for DateInsertFileCreated.
	/// </summary>
	[Serializable()]
	public class DateInsertFileCreated : DateInsertCurrent
	{
		public override string Name
		{
			get { return "Insert file created date";}
		}

		public override string Help
		{
			get { return "Insert the date and time of when the file was created.";}
		}

		public override void Perform(ListViewItem lvi)
		{
			FileName f = (FileName)lvi.Tag;
			DateTime date = f.Created;
			Perform(lvi, date);
		}
	}
}
