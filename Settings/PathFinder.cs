using System;
using System.IO;
using System.Windows.Forms;

namespace RenamerNG.Settings
{
	/// <summary>
	/// Method for finding a settings file:
	/// 1. Search program directory
	/// 2. Search user application data directory
	/// 3. Search user application data directories from older versions of the program.
	/// When saving, save in program dir if settings file is there, else save in user
	/// application data directory.
	/// </summary>
	public class PathFinder
	{
		private PathFinder()
		{
		}

		public static string SaveFile(string file)
		{
			string path = Application.StartupPath + file;

			if (File.Exists(path)) return path;

			path = Application.UserAppDataPath + file;

			return path;
		}

		public static string OpenFile(string file)
		{
			string path = Application.StartupPath + file;

			if (File.Exists(path)) return path;

			path = Application.UserAppDataPath + file;

			if (File.Exists(path)) return path;

			DirectoryInfo[] dirs = Directory.GetParent(Application.UserAppDataPath).GetDirectories();

			int selected = -1;
			for (int pos = 0 ; pos < dirs.Length ; pos++)
			{
				//If there is no config file in the dir, skip the dir
				if (!File.Exists(dirs[pos].FullName + file)) continue;

				if (selected == -1)
				{
					selected = pos;
				}
				else
				{
					string s1, s2;

					s1 = dirs[selected].Name;
					s2 = dirs[pos].Name;

					for (int i = 0 ; i < 4 ; i++)
					{
						int i1, i2;

						i1 = int.Parse(s1.Split('.')[i]);
						i2 = int.Parse(s2.Split('.')[i]);

						if (i1 > i2) break;

						if (i1 < i2)
						{
							selected = pos;
							break;
						}
					}
				}
			}

			if (selected == -1)
				return Application.UserAppDataPath + file;
			else
				return dirs[selected].FullName + file;
		}
	}
}
