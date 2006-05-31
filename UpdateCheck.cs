using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace RenamerNG
{
    static class UpdateCheck
    {
        static string url;
        static long lastCheck;
        static bool check;
        static bool checkBeta;
        static string version;
        static string betaVersion;

        public static long LastCheck
        {
            get
            {
                return lastCheck;
            }
        }

        public static void Check(string urlArg, long lastCheckArg, bool checkArg, bool checkBetaArg, string versionArg, string betaVersionArg)
        {
            url = urlArg;
            lastCheck = lastCheckArg;
            check = checkArg;
            checkBeta = checkBetaArg;
            version = versionArg;
            betaVersion = betaVersionArg;

            if (check || checkBeta)
            {
                Thread t = new Thread(new ThreadStart(Checker));
                t.Start();
            }
        }

        private static int CompareVersion(string s1, string s2)
        {
            string[] parts1 = s1.Split('.');
            string[] parts2 = s2.Split('.');

            if (parts1.Length != 4 || parts2.Length != 4) throw new Exception("Invalid version strings.");

            for (int i = 0; i < parts1.Length; i++)
            {
                if (int.Parse(parts1[i]) > int.Parse(parts2[i])) return 1;
                if (int.Parse(parts1[i]) < int.Parse(parts2[i])) return -1;
            }

            return 0;
        }

        private static void Checker()
        {
            try
            {
                HttpWebResponse response = (HttpWebResponse)((HttpWebRequest)WebRequest.Create(url)).GetResponse();
                using (response)
                {
                    StreamReader r = new StreamReader(response.GetResponseStream());
                    using (r)
                    {
                        string newVersion = r.ReadLine();
                        string newBetaVersion = r.ReadLine();

                        bool found = false;
                        if (check)
                        {
                            if (CompareVersion(newVersion, version) > 0)
                            {
                                found = true;
                                DialogResult res = MessageBox.Show("A new version is available. Download it now?", "Info", MessageBoxButtons.YesNo);
                                if (res == DialogResult.Yes)
                                {
                                    System.Diagnostics.Process.Start(@"http://www.albert.nu/programs/renamerng/renamerng.zip");
                                }
                            }
                        }

                        if (!found && checkBeta)
                        {
                            if (CompareVersion(newBetaVersion, betaVersion) > 0)
                            {
                                found = true;
                                DialogResult res = MessageBox.Show("A new BETA version is available. Download it now?", "Info", MessageBoxButtons.YesNo);
                                if (res == DialogResult.Yes)
                                {
                                    System.Diagnostics.Process.Start(@"http://www.albert.nu/programs/renamerng/renamerng-beta.zip");
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                //Do nothing, fail silently
            }
        }
    }
}
