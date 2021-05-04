using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet
{
    public class RawFilterProject : FilterProject
    {
        public byte[] Data { get; set; }

        public RawFilterProject(string name, byte[] data)
        {
            Name = name;
            Data = data;
        }

        public RawFilterProject(BinaryReader reader)
        {
            var header = reader.ReadBytes(18).ToSjisString();
            if(header != Header)
            {
                throw new FileFormatException("Cannot find FilterProject header.");
            }
            var nameLength = reader.ReadInt32();
            Name = reader.ReadBytes(nameLength).ToCleanSjisString();
            var dataSize = reader.ReadInt32();
            Data = new byte[dataSize];
            AviUtlProject.Decomp(reader, Data);
        }

        public override byte[] DumpData()
        {
            return Data;
        }
    }
}
