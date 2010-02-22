using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for Restore.
	/// </summary>
	[Serializable()]
	public class Restore : Operation
	{
        public Restore()
		{
		}

		public override string Name
		{
			get{ return "Restore";}
		}

		public override string Group
		{
			get{ return "Restoring";}
		}

		public override string Help
		{
            get { return "Set the filename to that of the restore point."; }
		}

		public override void Perform(ListViewItem lvi)
		{
			FileName f = (FileName)lvi.Tag;

            f.Restore();
		}
	}
}
