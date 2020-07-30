using System;
using System.Collections;
using System.IO;
using System.Text;

namespace BipEmulator.Host
{
    public class FtFile
    {
        const string SIGNATURE = "NEZK";
        public byte Version { get; private set; }
        public Hashtable Letters { get; } = new Hashtable();

        public static FtFile Load(string filename)
        {
            var ftFile = new FtFile();

            using (var br = new BinaryReader(File.OpenRead(filename)))
            {
                var signature = Encoding.ASCII.GetString(br.ReadBytes(4));
                if (signature != SIGNATURE)
                    throw new ArgumentException($"Wrong signature: {signature}");

                ftFile.Version = br.ReadByte();
                br.BaseStream.Position = 0x20;
                int letterBlockCount = br.ReadUInt16();
                var hashLen = ftFile.Version == 8 ? 1 : 2;

                for (var block = 0; block < 6 * letterBlockCount; block += 6)
                {
                    int letterCodeStart = br.ReadUInt16();
                    int letterCodeStop = br.ReadUInt16();
                    int lettersOffset = br.ReadUInt16();

                    var saveOffset = br.BaseStream.Position;
                    // устанавливаем смещение дял чтения блока данных набора символов
                    // с кодами от letterCodeStart до letterCodeStop
                    br.BaseStream.Position = 6 * letterBlockCount + 0x22 + (34 - hashLen) * lettersOffset;

                    for (var l = letterCodeStart; l <= letterCodeStop; l++)
                    {
                        // 33 байт блок
                        if (ftFile.Version == 8)
                        {
                            ftFile.Letters.Add(l, br.ReadBytes(32));
                            // hash???
                            br.ReadByte();
                        }
                        // 32 байт блок
                        else
                        {
                            ftFile.Letters.Add(l, br.ReadBytes(30));
                            // hash???
                            br.ReadUInt16();
                        }
                    }
                    br.BaseStream.Position = saveOffset;
                }
            }

            return ftFile;
        }
    }
}
