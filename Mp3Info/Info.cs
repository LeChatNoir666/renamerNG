using System;
using System.IO;

namespace JPSearch.Mp3Info
{
    [Serializable()]
    public class Info
    {
        int length;
        bool vbr;
        int bitRate;
        bool id3v2;
        bool id3v1;

        public int Length
        {
            get { return length; }
        }

        public bool VBR
        {
            get { return vbr; }
        }

        public int BitRate
        {
            get { return bitRate; }
        }

        public bool Id3v2
        {
            get { return id3v2; }
        }

        public bool Id3v1
        {
            get { return id3v1; }
        }

        public Info(int length, bool vbr, int bitRate, bool id3v2, bool id3v1)
        {
            this.length = length;
            this.vbr = vbr;
            this.bitRate = bitRate;
            this.id3v2 = id3v2;
            this.id3v1 = id3v1;
        }

        public override string ToString()
        {
            string str = String.Format("Length {0}, VBR {1}, BitRate {2}, Id3v2 {3}, Id3v1 {4}",
                length, vbr, bitRate, id3v2, id3v1);
            return str;
        }
    }
}
