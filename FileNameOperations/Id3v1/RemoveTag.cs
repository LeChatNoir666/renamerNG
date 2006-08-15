using System;
using System.Windows.Forms;
using System.IO;
using RenamerNG;

namespace RenamerNG.FileNameOperations.Id3v1
{
	/// <summary>
	/// Summary description for Id3v1ReadTag.
	/// </summary>
	[Serializable()]
	public class RemoveTag : Operation
	{
        public RemoveTag()
		{
		}

		public override string Name
		{
            get { return "Id3v1 Remove tag"; }
		}

		public override string Group
		{
			get{ return "Id3v1";}
		}

		public override string Help
		{
			get{ return "Remove the id3 v1 tag from a mp3 file.";}
		}

		public override int ParameterCount
		{
			get{ return 0;}
		}

		public override Shortcut Key
		{
			get{ return Shortcut.None;}
		}

		public override GUIElement[] Gui
		{
			get
			{
                return null;
            }
		}

		public override void Perform(ListViewItem lvi)
		{
            FileName f = (FileName)lvi.Tag;

            try
            {
                FileAttributes fa = File.GetAttributes(f.Filename);
                if ((fa & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    File.SetAttributes(f.Filename, fa & ~FileAttributes.ReadOnly);

                FileStream fs = new FileStream(f.Filename, FileMode.Open, FileAccess.ReadWrite);
                using (fs)
                {
                    fs.Seek(-Id3v1Tag.Size, SeekOrigin.End);
                    byte[] tagBuffer = new byte[Id3v1Tag.Size];
                    int bytes = fs.Read(tagBuffer, 0, Id3v1Tag.Size);
                    if (bytes != Id3v1Tag.Size) throw new IOException("Failed to read tag.");

                    if (Id3v1.Id3v1Tag.IsValid(tagBuffer))
                    {
                        fs.SetLength(fs.Length - Id3v1.Id3v1Tag.Size);
                        f.GetSize();
                    }
                }

                f.Success = true;
            }
            catch
            {
                f.Success = false;
            }
		}
	}
}
