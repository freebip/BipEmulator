using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BipEmulator.Host
{
    public class ResFile
    {
        const string SIGNATURE = "NERES";

        public byte Version { get; private set; }
        public List<byte[]> Resources { get; private set; }

        public static ResFile Load(string fileName)
        {
            var resFile = new ResFile();
            using (var br = new BinaryReader(File.OpenRead(fileName)))
            {
                var signature = Encoding.ASCII.GetString(br.ReadBytes(5));
                if (signature != SIGNATURE)
                    throw new ArgumentException($"Unknown signature: {signature}");
                resFile.Version = br.ReadByte();

                br.BaseStream.Position = 0x20;
                var resCount = br.ReadUInt32();

                var offsetTable = new uint[resCount];
                for (var i = 0; i < resCount; i++)
                    offsetTable[i] = br.ReadUInt32();

                var resourceFileOffset = br.BaseStream.Position;
                var fileLength = br.BaseStream.Length;

                resFile.Resources = new List<byte[]>();
                for (var i = 0; i < resCount; i++)
                {
                    var offset = resourceFileOffset + offsetTable[i];
                    var nextOffset = i + 1 < resCount ? resourceFileOffset + offsetTable[i+1] : fileLength;
                    var resLength = nextOffset - offset;
                    if (resLength < 0 || resLength > fileLength)
                        throw new ArgumentException($"Format error. Offset: {offset}");
                    resFile.Resources.Add(br.ReadBytes((int)resLength));
                }
            }

            return resFile;
        }
    }
}
