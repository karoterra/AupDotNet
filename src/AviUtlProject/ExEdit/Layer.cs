using System;
using Karoterra.AviUtlProject.Extensions;

namespace Karoterra.AviUtlProject.ExEdit
{
    public class Layer
    {
        public static readonly int Size = 76;

        public uint SceneIndex { get; set; }
        public uint LayerIndex { get; set; }
        public LayerFlag Flag { get; set; }
        public string Name { get; set; }

        public Layer(ReadOnlySpan<byte> data)
        {
            if (data.Length < Size)
            {
                throw new ArgumentException($"data length requires {Size} bytes.");
            }
            SceneIndex = data.Slice(0, 4).ToUInt32();
            LayerIndex = data.Slice(4, 4).ToUInt32();
            Flag = (LayerFlag)data.Slice(8, 4).ToUInt32();
            Name = data.Slice(12).ToSjisString().CutNull();
        }

        public void Dump(Span<byte> data)
        {
            SceneIndex.ToBytes().CopyTo(data);
            LayerIndex.ToBytes().CopyTo(data.Slice(4));
            ((uint)Flag).ToBytes().CopyTo(data.Slice(8));
            Name.ToSjisBytes().CopyTo(data.Slice(12));
        }
    }
}
