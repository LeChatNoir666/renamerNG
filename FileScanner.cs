using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace RenamerNG
{
	/// <summary>
	/// Summary description for FileScanner.
	/// </summary>
	public delegate void AddItemDelegate(FileName f);

	public class FileScanner
	{
		bool editExt;
		bool files;
		bool dirs;
		bool recurse;
		bool successful;
		string pattern;
		AddItemDelegate d;

		string path;
		int depth;

		FrmProgress progress = null;

		public FileScanner(string path, bool files, bool dirs, bool recurse, string pattern, bool editExt, AddItemDelegate d)
		{
			this.path = path;
			this.files = files;
			this.dirs = dirs;
			this.recurse = recurse;
			this.pattern = pattern;
			this.d = d;
			this.editExt = editExt;
		}

		public bool Scan()
		{
			successful = true;

			try
			{
				if (recurse)
				{
					depth = 0;
                    progress = new FrmProgress();
                    Thread t = new Thread(new ThreadStart(PerformRecursiveScan));
                    t.Start();
                    progress.ShowDialog();
                }
				else
				{
                    PerformScan();
				}
			}
			catch (UnauthorizedAccessException)
			{
				//Do nothing, catches problems when scanning "NTFS System Volume Information"
			}
			catch (Exception ex)
			{
				successful = false;
				FrmMain.ErrorMessage(ex.Message);
			}

			return successful;
		}

		private void PerformScan()
		{
			try
			{
				if (dirs)
				{
					foreach (string s in Directory.GetDirectories(path))
					{
						FileName f = new FileName(s, editExt);
						d(f);
					}
				}

				if (files)
				{
					if (pattern != null && pattern != "")
					{
						foreach (string s in Directory.GetFiles(path, pattern))
						{
							FileName f = new FileName(s, editExt);
							d(f);
						}
					}
					else
					{
						foreach (string s in Directory.GetFiles(path))
						{
							FileName f = new FileName(s, editExt);
							d(f);
						}
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
				//Do nothing, catches problems when scanning "NTFS System Volume Information"
			}
		}

		private void PerformRecursiveScan()
		{
			try
			{
                while (!progress.Visible) Thread.Sleep(10);

                if (progress.Cancel) return;

				string[] paths = Directory.GetDirectories(path);

				if (depth == 0)
				{
					progress.SetMinimum(0);
					progress.SetMaximum(paths.Length);
					progress.SetValue(0);
				}

				foreach (string p in paths)
				{
					string temp = path;
					path = p;

					depth++;
					PerformRecursiveScan();
					depth--;

                    if (depth == 0) progress.IncreaseValue();
					path = temp;
				}

				PerformScan();

				if (depth == 0)
				{
					if (progress.Cancel) successful = false;
                    progress.Close();
                }
			}
			catch (UnauthorizedAccessException)
			{
				//Do nothing, catches problems when scanning "NTFS System Volume Information"
			}
		}
	}
}
