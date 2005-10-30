using System;
using System.Collections;
using System.IO;

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
		string name;
		string newName;
		string undo;
		string restorePoint;
		string path;

		long size;

		bool updated;
		bool success;

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
					case ListColumns.NewName:
						data = NewName;
						break;
					case ListColumns.OldName:
						data = Name;
						break;
					case ListColumns.Size:
						if (IsDirectory)
							data = "Dir";
						else
							data = Size.ToString();
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
					default:
						throw new IndexOutOfRangeException();
				}

				return data;
			}
		}

		FileAttributes attributes;

		public string Name
		{
			get { return name; }
		}

		public string Path
		{
			get { return path; }
		}

		public string PathName
		{
			get { return path + '\\' + name; }
		}

		public long Size
		{
			get { return size; }
		}

		public bool Changed
		{
			get { return name != newName; }
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
			set {
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

		public void Undo()
		{
			if (undo != newName)
			{
				newName = undo;
				updated = true;
			}
			else
				updated = false;

			success = true;
		}

		public void SetRestorePoint()
		{
			restorePoint = newName;
		}

		public void Restore()
		{
			if (newName != restorePoint)
			{
				newName = restorePoint;
				updated = true;
			}
			else
				updated = false;

			success = true;
		}

		public FileName(string file)
		{
			FileInfo f = new FileInfo(file);

			restorePoint = undo = newName = name = f.Name;
			path = f.DirectoryName;

			if (path[path.Length - 1] != '\\')
				path += '\\';

			attributes = f.Attributes;

			if (IsDirectory)
				size = -1;
			else
				size = f.Length;

			created = f.CreationTime;
			lastAccess = f.LastAccessTime;
			lastWrite = f.LastWriteTime;

			updated = false;
			success = true;
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
						File.Move(path + name, path + newName);
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