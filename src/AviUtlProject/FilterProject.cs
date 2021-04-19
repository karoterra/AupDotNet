using System.IO;
using System.Text;
using Karoterra.AviUtlProject.Extensions;

namespace Karoterra.AviUtlProject
{
    public abstract class FilterProject
    {
        protected static readonly string Header = "FilterProject 0.1\0";
        protected static readonly Encoding Sjis;
        public string Name { get; set; }

        static FilterProject()
        {
            Sjis = Encoding.GetEncoding(932);
        }

        public abstract byte[] DumpData();

        public void Write(BinaryWriter writer)
        {
            writer.Write(Sjis.GetBytes(Header));
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
