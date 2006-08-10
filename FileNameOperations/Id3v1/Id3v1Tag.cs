using System;
using System.Collections.Generic;
using System.Text;

namespace RenamerNG.FileNameOperations.Id3v1
{
    class Id3v1Tag
    {
        public const int Size = 128;
        string tag;

        public Id3v1Tag(char[] tagBuffer)
        {
            tag = new string(tagBuffer);
        }

        public bool Valid
        {
            get
            {
                return tag.StartsWith("TAG");
            }
        }

        public string Song
        {
            get
            {
                return tag.Substring(3, 30).TrimEnd(new char[] { '\0', ' ' });
            }
        }

        public string Artist
        {
            get
            {
                return tag.Substring(33, 30).TrimEnd(new char[]{'\0', ' '});
            }
        }

        public string Album
        {
            get
            {
                return tag.Substring(63, 30).TrimEnd(new char[] { '\0', ' ' });
            }
        }

        public string Year
        {
            get
            {
                return tag.Substring(93, 4).TrimEnd(new char[] { '\0', ' ' });
            }
        }

        public string Comment
        {
            get
            {
                if (tag[125] == '\0')
                    return tag.Substring(97, 28).TrimEnd(new char[] { '\0', ' ' });
                else
                    return tag.Substring(97, 30).TrimEnd(new char[] { '\0', ' ' });
            }
        }

        public string Genre
        {
            get
            {
                int i = (int)tag[127];
                if (i < 0 || i >= genre.Length) return "";
                return genre[i];
            }
        }

        public string Track
        {
            get
            {
                if (tag[125] == '\0')
                    return ((int)tag[126]).ToString();
                else
                    return "";
            }
        }

        public string this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return Song;
                    case 1:
                        return Artist;
                    case 2:
                        return Album;
                    case 3:
                        return Year;
                    case 4:
                        return Comment;
                    case 5:
                        return Track;
                    case 6:
                        return Genre;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public int Length
        {
            get
            {
                return 7;
            }
        }

        static readonly string[] genre = new string[]
        {
            "Blues",
            "Classic Rock",
            "Country",
            "Dance",
            "Disco",
            "Funk",
            "Grunge",
            "Hip-Hop",
            "Jazz",
            "Metal",
            "New Age",
            "Oldies",
            "Other",
            "Pop",
            "R&B",
            "Rap",
            "Reggae",
            "Rock",
            "Techno",
            "Industrial",
            "Alternative",
            "Ska",
            "Death Metal",
            "Pranks",
            "Soundtrack",
            "Euro-Techno",
            "Ambient",
            "Trip-Hop",
            "Vocal",
            "Jazz+Funk",
            "Fusion",
            "Trance",
            "Classical",
            "Instrumental",
            "Acid",
            "House",
            "Game",
            "Sound Clip",
            "Gospel",
            "Noise",
            "Alternative Rock",
            "Bass",
            "Punk",
            "Space",
            "Meditative",
            "Instrumental Pop",
            "Instrumental Rock",
            "Ethnic",
            "Gothic",
            "Darkwave",
            "Techno-Industrial",
            "Electronic",
            "Pop-Folk",
            "Eurodance",
            "Dream",
            "Southern Rock",
            "Comedy",
            "Cult",
            "Gangsta",
            "Top 40",
            "Christian Rap",
            "Pop/Funk",
            "Jungle",
            "Native US",
            "Cabaret",
            "New Wave",
            "Psychadelic",
            "Rave",
            "Showtunes",
            "Trailer",
            "Lo-Fi",
            "Tribal",
            "Acid Punk",
            "Acid Jazz",
            "Polka",
            "Retro",
            "Musical",
            "Rock & Roll",
            "Hard Rock",
            "Folk",
            "Folk-Rock",
            "National Folk",
            "Swing",
            "Fast Fusion",
            "Bebob",
            "Latin",
            "Revival",
            "Celtic",
            "Bluegrass",
            "Avantgarde",
            "Gothic Rock",
            "Progressive Rock",
            "Psychedelic Rock",
            "Symphonic Rock",
            "Slow Rock",
            "Big Band",
            "Chorus",
            "Easy Listening",
            "Acoustic",
            "Humour",
            "Speech",
            "Chanson",
            "Opera",
            "Chamber Music",
            "Sonata",
            "Symphony",
            "Booty Bass",
            "Primus",
            "Porn Groove",
            "Satire",
            "Slow Jam",
            "Club",
            "Tango",
            "Samba",
            "Folklore",
            "Ballad",
            "Power Ballad",
            "Rhytmic Soul",
            "Freestyle",
            "Duet",
            "Punk Rock",
            "Drum Solo",
            "Acapella",
            "Euro-House",
            "Dance Hall",
            "Goa",
            "Drum & Bass",
            "Club-House",
            "Hardcore",
            "Terror",
            "Indie",
            "BritPop",
            "Negerpunk",
            "Polsk Punk",
            "Beat",
            "Christian Gangsta",
            "Heavy Metal",
            "Black Metal",
            "Crossover",
            "Contemporary C",
            "Christian Rock",
            "Merengue",
            "Salsa",
            "Thrash Metal",
            "Anime",
            "JPop",
            "SynthPop"
        };
    }
}
