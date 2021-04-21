using System.IO;
using System.Text;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet
{
    public abstract class FilterProject
    {
        protected static readonly string Header = "FilterProject 0.1\0";
        public string Name { get; set; }

        public abstract byte[] DumpData();

        public void Write(BinaryWriter writer)
        {
            writer.Write(Header.ToSjisBytes());
            var name = Name.ToSjisBytes();
            writer.Write(name.Length + 1);
            writer.Write(name);
            writer.Write((byte)0);
            var data = DumpData();
            writer.Write(data.Length);
            AviUtlProject.Comp(writer, data);
        }
    }
}
