using System;
using System.IO;

namespace JPSearch.Mp3Info
{
    internal class Header
    {
        byte[] head = new byte[4];

        public Header(byte[] header, int pos)
        {
            for (int i = 0; i < 4; i++)
                head[i] = header[i + pos];
        }

        public bool Valid()
        {
            if (head[0] == 0xFF && (head[1] & 0xE0) == 0xE0 &&
                        Version >= 2 && Layer != 0 && SampleFrequency > 0 && BitRate > 0)
                return true;
            else
            {
                return false;
            }
        }

        #region Private properties
        private int Version
        {
            get
            {
                return (head[1] & 0x18) >> 3;
            }
        }

        private int Layer
        {
            get
            {
                return (head[1] & 0x06) >> 1;
            }
        }

        private bool Protection
        {
            get
            {
                return (head[1] & 0x01) == 1;
            }
        }

        private int Rate
        {
            get
            {
                return (head[2] & 0xF0) >> 4;
            }
        }

        private int Freq
        {
            get
            {
                return (head[2] & 0x0C) >> 2;
            }
        }

        private int Pad
        {
            get
            {
                return (head[2] & 0x02) >> 1;
            }
        }

        private int Mode
        {
            get
            {
                return (head[3] & 0xC0) >> 6;
            }
        }
        #endregion

        public override string ToString()
        {
            return String.Format("V {0}, L {1}, Prot {2}, BitRate {3}, SamplingFreq {4}, Pad {5}, Mode {6}",
                Version, Layer, Protection, BitRate, SampleFrequency, Pad, Mode);
        }

        public int FrameSize
        {
            get
            {
                return 144 * BitRate * 1000 / SampleFrequency + Pad;
            }
        }

        public int SampleFrequency
        {
            get
            {
                int[,] frequencys = new int[4, 4]
                {
                    {11025, 12000,  8000, -1},
                    {-1, -1, -1, -1},
                    {22050, 24000, 16000, -1},
                    {44100, 48000, 32000, -1}
                };

                return frequencys[Version, Freq];
            }
        }

        public int BitRate
        {
            get
            {
                int[,] bitRates = new int[5, 16]
                {
                    {-2, 32, 64, 96, 128, 160, 192, 224, 256, 288, 320, 352, 384, 416, 448, -1},
                    {-2, 32, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320, 384, -1},
                    {-2, 32, 40, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320, -1},
                    {-2, 32, 48, 56, 64, 80, 96, 112, 128, 144, 160, 176, 192, 224, 256, -1},
                    {-2, 8, 16, 24, 32, 40, 48, 56, 64, 80, 96, 112, 128, 144, 160, -1}
                };

                int[,] bitRateColumn = new int[4, 4]
                {
                    {-1, -1, -1, -1},
                    {-1, -1, -1, -1},
                    {-1, 4, 4, 3},
                    {-1, 2, 1, 0}
                };

                int col = bitRateColumn[Version, Layer];
                if (col < 0) return -1;

                return bitRates[col, Rate];
            }
        }
    }
}
