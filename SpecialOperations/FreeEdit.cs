using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.SpecialOperations
{
	/// <summary>
	/// Summary description for FreeEdit.
	/// </summary>
	public class FreeEdit : Operation
	{
		int index;
		string[] lines;

		public string[] Lines
		{
			set { lines = value; }
		}

		public FreeEdit()
		{
		}

		public override string Name
		{
			get { return "Free edit";}
		}

		public override string Group
		{
			get { return "";}
		}

		public override string Help
		{
			get { return "Use a normal text editor to edit the file names.";}
		}

		public override bool Recordable
		{
			get { return false; }
		}


		/// <summary>
		/// Override Init because this operation needs to
		/// initialize the index value that indicates the
		/// current line to use.
		/// </summary>
		public override void Init()
		{
			index = 0;
		}

		public override void Perform(ListViewItem lvi)
		{
			FileName f = (FileName)lvi.Tag;
			f.NewName = lines[index++];
		}
	}
}
