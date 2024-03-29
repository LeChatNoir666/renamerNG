using System;
using System.Collections;
using System.IO;
using JPSearch.Mp3Info;

namespace RenamerNG
{
	public class ReversePathComparer : IComparer
	{
		public int Compare(object o1, object o2)
		{
			FileName f1 = (FileName)o1;
			FileName f2 = (FileName)o2;
			return String.Compare(f2.Path, f1.Path);
		}
	}

	/// <summary>
	/// Summary description for FileName.
	/// </summary>
	public class FileName : ICloneable
	{
		string ext;
		string name;
		string newName;
		string undo;
		string restorePoint;
		string path;

		long size;

		bool updated;
		bool success;

        string mp3BitRate;

		DateTime created;
		DateTime lastAccess;
		DateTime lastWrite;

		private static ListColumns[] defaultColumns = new ListColumns[]{ListColumns.NewName, ListColumns.Size};
		private static int[] defaultColumnWidths = new int[]{280,70};

		public static ListColumns[] DefaultColumns
		{
			get { return defaultColumns; }
		}

		public static int[] DefaultColumnWidths
		{
			get { return defaultColumnWidths; }
		}

        public void Delete()
        {
            if (IsDirectory)
                Directory.Delete(this.Filename, true);
            else
                File.Delete(this.Filename);
        }

		public string this[ListColumns column]
		{
			get 
			{
				string data;

				switch (column)
				{
					case ListColumns.Changed:
						data = "";
						if (Changed) data = "C";
						break;
					case ListColumns.Updated:
						data = "";
						if (Updated) data = "U";
						break;
					case ListColumns.Success:
						data = "";
						if (Success) data = "S";
						break;
					case ListColumns.Ext:
						data = ext;
						break;
					case ListColumns.NewName:
						data = NewName;
						break;
					case ListColumns.OldName:
						data = Name;
						break;
					case ListColumns.Size:
                        data = "";
						if (IsDirectory)
							data = "D: ";
						
						//data += Size.ToString();
                        data += string.Format("{0:#,0}", Size);
						break;
					case ListColumns.Created:
						data = Created.ToString();
						break;
					case ListColumns.Modified:
						data = LastWrite.ToString();
						break;
					case ListColumns.Accessed:
						data = LastAccess.ToString();
						break;
					case ListColumns.Path:
						data = Path;
						break;
                    case ListColumns.ChangeFromRestorePoint:
                        data = "";
                        if (ChangedFromRestorepoint) data = "CFRP";
                        break;
                    case ListColumns.RestorePoint:
                        data = restorePoint;
                        break;
                    case ListColumns.MP3BitRate:
                        data = mp3BitRate;
                        break;
					default:
						throw new IndexOutOfRangeException();
				}

				return data;
			}
		}

		FileAttributes attributes;

        public string Filename
        {
            get { return Path + Name + Ext; }
        }

		public string Name
		{
			get { return name; }
		}

		public string Path
		{
			get { return path; }
		}

        public long Size
		{
			get { return size; }
		}

        private static long GetDirectorySize(string file)
        {
            long ret = 0;
            string[] dirs = Directory.GetDirectories(file);
            foreach (string d in dirs)
            {
                try
                {
                    ret += GetDirectorySize(d);
                }
                catch
                {
                }
            }

            string[] files = Directory.GetFiles(file);
            foreach (string f in files)
            {
                ret += GetSize(f);
            }

            return ret;
        }

        private static long GetSize(string file)
        {
            try
            {
                FileInfo fi = new FileInfo(file);
                return fi.Length;
            }
            catch
            {
                return 0;
            }
        }

        public void GetSize()
        {
            size = GetSize(this.Filename);
        }

		public bool Changed
		{
			get { return name != newName; }
		}

        public bool ChangedFromRestorepoint
        {
            get { return newName != restorePoint; }
        }

		public bool Updated
		{
			get { return updated; }
			set { updated = value; }
		}

		public bool Success
		{
			get { return success; }
			set { success = value; }
		}

		public DateTime Created
		{
			get { return created; }
		}

		public DateTime LastAccess
		{
			get { return lastAccess; }
		}

		public DateTime LastWrite
		{
			get { return lastWrite; }
		}

		public string NewName
		{
			get { return newName; }
			set 
			{
				undo = newName;
				
				if (newName != value)
				{
					newName = value;
					updated = true;
				}
				else
					updated = false;

				success = true;
			}
		}

        public string Mp3BitRate
        {
            get { return mp3BitRate; }
        }

		public string Ext
		{
			get { return ext; }
		}

		public void Undo()
		{
			if (undo != newName)
			{
                string temp = newName;
				newName = undo;
                undo = temp;
				updated = true;
			}
			else
				updated = false;

			success = true;
		}

		public void SetRestorePoint()
		{
			restorePoint = NewName;
		}

		public void Restore()
		{
            NewName = restorePoint;
		}

		public FileName(string file, bool editExt)
		{
			FileInfo f = new FileInfo(file);
			attributes = f.Attributes;

			//Directories don't have extensions
			//Filenames that start with a . are not extensions
			if (editExt || IsDirectory || f.Name.Length == f.Extension.Length)
			{
				ext = "";
				restorePoint = undo = newName = name = f.Name;
			}
			else
			{
				ext = f.Extension;
				restorePoint = undo = newName = name = f.Name.Substring(0, f.Name.Length - f.Extension.Length);
			}
			path = f.DirectoryName;

			if (path[path.Length - 1] != '\\')
				path += '\\';

			if (IsDirectory)
				size = GetDirectorySize(Filename);
			else
				size = f.Length;

			created = f.CreationTime;
			lastAccess = f.LastAccessTime;
			lastWrite = f.LastWriteTime;

			updated = false;
			success = true;

            if (f.Extension.ToLower() == ".mp3")
            {
                try
                {
                    JPSearch.Mp3Info.Info i = Parser.GetInfo(f.FullName);
                    if (i.BitRate == -1) throw new Exception();

                    mp3BitRate = i.BitRate.ToString();
                    if (i.VBR) mp3BitRate += " vbr";
                }
                catch
                {
                    mp3BitRate = "ERR";
                }
            }
            else
            {
                mp3BitRate = "-";
            }
		}

		public bool ReadOnly
		{
			get { return (attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly; }
		}

		public bool IsDirectory
		{
			get { return (attributes & FileAttributes.Directory) == FileAttributes.Directory; }
		}

		public bool Hidden
		{
			get { return (attributes & FileAttributes.Hidden) == FileAttributes.Hidden; }
		}

		public object Clone()
		{
			return this.MemberwiseClone();
		}

		public void Rename()
		{
			if (Changed)
			{
				try
				{
					if (IsDirectory)
					{
						if (name.ToLower() == newName.ToLower())
						{
							//If new name is equal to old name except for case a work around is required

							//Find new unique file name
							Random r = new Random();
							string random = "";
							do 
							{
								random += r.NextDouble().ToString();
							} while (Directory.Exists(path + newName + random));

							//Temporarily rename to unique file name
							Directory.Move(path + name, path + newName + random);

							//Rename to correct file name
							Directory.Move(path + newName + random, path + newName);
						}
						else
						{
							//Rename the directory
							Directory.Move(path + name, path + newName);
						}
					}
					else
					{
						//Add the extension both to the original name and new name
						File.Move(path + name + ext, path + newName + ext);
					}

					name = newName;
					success = true;
				}
				catch
				{
					success = false;
				}

				updated = true;
			}
			else
				updated = false;
		}
	}
}
