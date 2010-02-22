using System;
using System.IO;

namespace JPSearch.Mp3Info
{
    public class Parser
    {
        public static Info GetInfo(string file)
        {
            if (file.Substring(file.Length - 4).ToLower() != ".mp3")
                throw new IOException("File extension must be '.mp3'.");

            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
            using (fs)
            {
                int length = 0;
                bool vbr = false;
                int bitRate = -1;
                bool id3v2 = false;
                bool id3v1 = false;

                byte[] headerStart = new byte[4];
                int len = fs.Read(headerStart, 0, 4);

                if (len != 4) throw new IOException("Failed to read header.");

                if (headerStart[0] == 0x49 && headerStart[1] == 0x44 && headerStart[2] == 0x33)
                {
                    //id3v2 tag found, read info, skip forward
                    id3v2 = true;

                    byte[] id3v2HeaderInfo = new byte[6];
                    len = fs.Read(id3v2HeaderInfo, 0, 6);

                    if (len != 6) throw new IOException("Failed to read header.");

                    len = (id3v2HeaderInfo[2] & 0x7F) * 128 * 128 * 128;
                    len += (id3v2HeaderInfo[3] & 0x7F) * 128 * 128;
                    len += (id3v2HeaderInfo[4] & 0x7F) * 128;
                    len += (id3v2HeaderInfo[5] & 0x7F);

                    if ((id3v2HeaderInfo[1] & 0x01) > 0)
                    {
                        //footer present jump 10 byte further forward
                        len += 10;
                    }

                    fs.Seek(len, SeekOrigin.Current);
                }
                else
                {
                    //No id3v2 tag found, start over
                    fs.Seek(0, SeekOrigin.Begin);
                }

                //Read frames
                int frames = 0;
                long firstFrame = -1;
                while (true)
                {
                    len = fs.Read(headerStart, 0, 4);

                    if (len < 4) break;

                    Header header = new Header(headerStart, 0);
                    if (header.Valid())
                    {
                        //mp3 frame
                        if (firstFrame == -1) firstFrame = fs.Position;
                        frames++;

                        if (bitRate == -1)
                            bitRate = header.BitRate;
                        else
                            if (bitRate != header.BitRate)
                                vbr = true;

                        if (vbr) bitRate += header.BitRate;

                        //Jump to next frame
                        const int endRead = 4 * 1024;
                        if (vbr)
                        {
                            //VBR
                            const int readPart = 3;
                            if (fs.Position > fs.Length / readPart && fs.Position < fs.Length - endRead)
                            {
                                //Quick seek
                                frames *= readPart;
                                bitRate *= readPart;
                                fs.Seek(endRead, SeekOrigin.End);
                            }
                            else
                            {
                                //Normal seek
                                fs.Seek(header.FrameSize - 4, SeekOrigin.Current);
                            }
                        }
                        else
                        {
                            //CBR
                            if (frames > 500 && fs.Position < fs.Length - endRead)
                            {
                                //Quick seek
                                frames = (int)(frames * (fs.Length - firstFrame) / (fs.Position - firstFrame));
                                fs.Seek(endRead, SeekOrigin.End);
                            }
                            else
                            {
                                //Normal seek
                                fs.Seek(header.FrameSize - 4, SeekOrigin.Current);
                            }
                        }
                    }
                    else if (headerStart[0] == 0x54 && headerStart[1] == 0x41 && headerStart[2] == 0x47)
                    {
                        //id3v1 tag
                        id3v1 = true;
                        break;
                    }
                    else
                    {
                        byte[] buffer = new byte[16 * 1024];
                        len = fs.Read(buffer, 0, buffer.Length);
                        bool found = len < buffer.Length; //If we are at the end it's ok
                        for (int pos = 0; pos < len - 4; pos++)
                        {
                            if (buffer[pos] == 0xFF && (buffer[pos] & 0xE0) == 0xE0 ||
                                buffer[pos] == 0x54 && buffer[pos] == 0x41) //A candidate
                            {
                                Header h = new Header(buffer, pos);
                                if (h.Valid() ||
                                    buffer[pos] == 0x54 && buffer[pos] == 0x41) //Better candidate
                                {
                                    fs.Seek(-len + pos, SeekOrigin.Current); //Jump back
                                    found = true;
                                    break;
                                }
                            }
                        }

                        if (!found)
                            throw new IOException("No valid frame header found");
                    }
                }

                length = frames * 26 / 1000;
                if (vbr) bitRate /= frames;

                return new Info(length, vbr, bitRate, id3v2, id3v1);
            }
        }
    }
}
