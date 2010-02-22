using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.FileNameOperations
{
	/// <summary>
	/// Summary description for RandomCase.
	/// </summary>
	[Serializable()]
	public class RandomCase : Operation
	{
		/*
		 * Use static Random object, creating a new Random object for
		 * each time "Perform" is called could possibly create non
		 * random file names.
		 * 
		 * This also makes sure that the Random object is not serialized
		 * which wouldn't make any sense.
		 */
		static Random r;

		public RandomCase()
		{
		}

		//Static initializer.
		static RandomCase()
		{
			r = new Random();
		}

		public override string Name
		{
			get{ return "rANdOm CAse";}
		}

		public override string Group
		{
			get{ return "Case";}
		}

		public override string Help
		{
			get{ return "Randomly change letters to upper or lower case.";}
		}

		public override void Perform(ListViewItem lvi)
		{
			FileName f = (FileName)lvi.Tag;

			if (f.NewName.Length == 0) return;

			System.Text.StringBuilder s = new System.Text.StringBuilder(f.NewName);
			for (int i = 0 ; i < s.Length ; i++)
				if (r.Next(2) == 0)
					s[i] = char.ToUpper(s[i]);
				else
					s[i] = char.ToLower(s[i]);

			f.NewName = s.ToString();
		}
	}
}
