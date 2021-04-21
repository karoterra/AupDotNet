using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit
{
    public class Scene
    {
        public static readonly int Size = 220;

        public uint SceneIndex { get; set; }
        public SceneFlag Flag { get; set; }
        public string Name { get; set; }
        public uint Height { get; set; }
        public uint Width { get; set; }

        public Scene(ReadOnlySpan<byte> data)
        {
            if (data.Length < Size)
            {
                throw new ArgumentException($"data length requires {Size} bytes.");
            }
            SceneIndex = data.Slice(0, 4).ToUInt32();
            Flag = (SceneFlag)data.Slice(4, 4).ToUInt32();
            Name = data.Slice(8, 64).ToSjisString().CutNull();
            Height = data.Slice(72, 4).ToUInt32();
            Width = data.Slice(76, 4).ToUInt32();
        }

        public void Dump(Span<byte> data)
        {
            SceneIndex.ToBytes().CopyTo(data);
            ((uint)Flag).ToBytes().CopyTo(data.Slice(4));
            Name.ToSjisBytes().CopyTo(data.Slice(8));
            Height.ToBytes().CopyTo(data.Slice(72));
            Width.ToBytes().CopyTo(data.Slice(76));
        }
    }
}
