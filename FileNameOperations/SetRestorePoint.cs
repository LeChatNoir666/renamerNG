using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for SetRestorePoint.
	/// </summary>
	[Serializable()]
	public class SetRestorePoint : Operation
	{
        public SetRestorePoint()
		{
		}

		public override string Name
		{
            get { return "Set restore point"; }
		}

		public override string Group
		{
			get{ return "Restoring";}
		}

		public override string Help
		{
            get { return "Remember the current filename."; }
		}

		public override void Perform(ListViewItem lvi)
		{
			FileName f = (FileName)lvi.Tag;

            f.SetRestorePoint();
		}
	}
}
