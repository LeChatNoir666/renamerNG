using System;
using System.Windows.Forms;
using System.Collections.Generic;
using RenamerNG;

namespace RenamerNG.SpecialOperations
{
	/// <summary>
	/// Summary description for RemoveLCS.
	/// </summary>
	public class RemoveLCS : Operation
	{
        private string remove;

        public string Remove
        {
            get { return remove; }
        }

		public RemoveLCS()
		{
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

		public void PreProcess(string[] sarr)
		{
            List<string> list = new List<string>();

            string s = sarr[0];
            foreach (string t in sarr)
            {
                if (t.Length < s.Length)
                    s = t;
            }

            for (int i = 0; i < s.Length; i++)
            {
                for (int j = 1 ; j <= s.Length && i+j <= s.Length ; j++)
                {
                    string p = s.Substring(i, j);
                    if (!list.Contains(p))
                        list.Add(p);
                }
            }

            foreach (string t in sarr)
            {
                for (int i = 0; i < list.Count; i++ )
                {
                    if (!t.Contains(list[i]))
                    {
                        list.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (list.Count <= 0)
            {
                remove = "";
                return;
            }

            s = list[0];
            foreach (string t in list)
            {
                if (t.Length > s.Length)
                    s = t;
            }

            remove = s;
        }

        public override void Perform(ListViewItem lvi)
		{
            if (remove == "") return;

			FileName f = (FileName)lvi.Tag;
			f.NewName = f.NewName.Replace(remove, "");
		}
	}
}
