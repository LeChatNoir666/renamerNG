using System;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using RenamerNG;

namespace RenamerNG.FileNameOperations.Id3v1
{
	/// <summary>
	/// Summary description for Id3v1ReadTag.
	/// </summary>
	[Serializable()]
	public class ReadTag : Operation
	{
        public ReadTag()
		{
            this[1] = "Empty";
		}

		public override string Name
		{
            get { return "Id3v1 Read tag"; }
		}

		public override string Group
		{
			get{ return "Id3v1";}
		}

		public override string Help
		{
			get{ return "Read the id3 v1 tag from a mp3 file.";}
		}

		public override int ParameterCount
		{
			get{ return 2;}
		}

		public override GUIElement[] Gui
		{
			get
			{
                return new GUIElement[]
				{
					new GUIElement(GUIElement.Types.TEXTBOX, "Filename", "", "The resulting file name use:\n$1 Current name\n$2 Song title\n$3 Artist\n$4 Album\n$5 Year\n$6 Comment\n$7 Track #\n$8 Genre"),
					new GUIElement(GUIElement.Types.TEXTBOX, "Missing tag text", "ALLOWEMPTY", "The text to insert in case the tag is missing")
				};
            }
		}

		public override void Perform(ListViewItem lvi)
		{
            FileName f = (FileName)lvi.Tag;

            if (f.IsDirectory)
            {
                f.Success = false;
                return;
            }

            try
            {
                StreamReader sr = new StreamReader(f.Filename, System.Text.Encoding.Default);
                using (sr)
                {
                    sr.BaseStream.Seek(-Id3v1Tag.Size, SeekOrigin.End);
                    char[] tagBuffer = new char[Id3v1Tag.Size];
                    int bytes = sr.ReadBlock(tagBuffer, 0, Id3v1Tag.Size);
                    if (bytes != Id3v1Tag.Size) throw new IOException("Failed to read tag.");

                    Id3v1Tag tag = new Id3v1Tag(tagBuffer);
                    if (!tag.Valid)
                    {
                        f.Success = false;
                        return;
                    }

                    string[] fields = new string[tag.Length];
                    for (int i = 0; i != tag.Length; ++i)
                    {
                        if (tag[i] == "")
                            fields[i] = this[1];
                        else
                            fields[i] = tag[i];
                    }

                    string separator = ""; //Some separator that doesn't occur in the fields.
                    for (int i = 0; i <= f.NewName.Length || i <= 30; ++i) separator += ";";

                    string source = f.NewName;
                    string pattern = "(.+)";
                    foreach (string s in fields)
                    {
                        source += separator + s;
                        pattern += separator + "(.+)";
                    }

                    f.NewName = Regex.Replace(source, pattern, this[0], RegexOptions.Singleline);
                }
            }
            catch
            {
                f.Success = false;
            }
		}
	}
}
