using System;
using System.Windows.Forms;
using RenamerNG;

namespace RenamerNG.SpecialOperations
{
	/// <summary>
	/// Summary description for RemoveLCS.
	/// </summary>
	public class RemoveLCS : Operation
	{
        string remove;

		public RemoveLCS()
		{
            remove = "";
		}

		public override string Name
		{
			get { return "Remove Largest Common Substring";}
		}

		public override string Group
		{
			get { return "";}
		}

		public override string Help
		{
			get { return "Remove the largest common substring.";}
		}

		public override int ParameterCount
		{
			get { return 0;}
		}

		public override Shortcut Key
		{
			get { return Shortcut.None;}
		}

		public override GUIElement[] Gui
		{
			get
			{
				return null;
			}
		}

		public override bool Recordable
		{
			get { return false; }
		}

        private static string LCS(string s1, string s2)
        {
            string lcs = "";

            for (int i = 0; i < s1.Length; i++)
            {
                for (int j = 0; j < s2.Length; j++)
                {
                    int k;
                    for (k = 0; i + k < s1.Length && j + k < s2.Length; k++)
                    {
                        if (s1[i + k] != s2[j + k]) break;
                    }

                    if (k > lcs.Length)
                        lcs = s1.Substring(i, k);
                }
            }

            return lcs;
        }

		public void PreProcess(string s1, string s2)
		{
            remove = LCS(s1, s2);

            MessageBox.Show(remove);
        }

        public void PreProcess(string s1)
        {
            remove = LCS(remove, s1);
            MessageBox.Show(remove);
        }

		public override void Perform(ListViewItem lvi)
		{
            if (remove == "") return;

			FileName f = (FileName)lvi.Tag;
			f.NewName = f.NewName.Replace(remove, "");
		}
	}
}
