using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit
{
    public class Scene
    {
        public static readonly int Size = 220;
        public static readonly int MaxNameLength = 64;

        public uint SceneIndex { get; set; }
        public SceneFlag Flag { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value.GetSjisByteCount() >= MaxNameLength)
                {
                    throw new MaxByteCountOfStringException(nameof(Name), MaxNameLength);
                }
                _name = value;
            }
        }

        public uint Height { get; set; }
        public uint Width { get; set; }
        public uint Field0x50 { get; set; }
        public uint Cursor { get; set; }
        public uint Zoom { get; set; }
        public uint TimeScroll { get; set; }
        public uint Field0x60 { get; set; }
        public uint Field0x64 { get; set; }
        public uint Field0x68 { get; set; }
        public uint Field0x6C { get; set; }
        public uint Field0x70 { get; set; }
        public uint Field0x74 { get; set; }
        public uint Field0x78 { get; set; }
        public uint Field0x7C { get; set; }
        public uint Field0x80 { get; set; }
        public uint Field0x84 { get; set; }
        public uint Field0x88 { get; set; }
        public uint Field0x8C { get; set; }
        public uint Field0x90 { get; set; }
        public uint Field0x94 { get; set; }
        public uint Field0x98 { get; set; }
        public uint LayerScroll { get; set; }

        public readonly byte[] Field0xA0_0xDC = new byte[60];

        public Scene(ReadOnlySpan<byte> data)
        {
            if (data.Length < Size)
            {
                throw new ArgumentException($"data length requires {Size} bytes.");
            }
            SceneIndex = data.Slice(0, 4).ToUInt32();
            Flag = (SceneFlag)data.Slice(4, 4).ToUInt32();
            Name = data.Slice(8, 64).ToCleanSjisString();
            Height = data.Slice(0x48, 4).ToUInt32();
            Width = data.Slice(0x4C, 4).ToUInt32();
            Field0x50 = data.Slice(0x50, 4).ToUInt32();
            Cursor = data.Slice(0x54, 4).ToUInt32();
            Zoom = data.Slice(0x58, 4).ToUInt32();
            TimeScroll = data.Slice(0x5C, 4).ToUInt32();
            Field0x60 = data.Slice(0x60, 4).ToUInt32();
            Field0x64 = data.Slice(0x64, 4).ToUInt32();
            Field0x68 = data.Slice(0x68, 4).ToUInt32();
            Field0x6C = data.Slice(0x6C, 4).ToUInt32();
            Field0x70 = data.Slice(0x70, 4).ToUInt32();
            Field0x74 = data.Slice(0x74, 4).ToUInt32();
            Field0x78 = data.Slice(0x78, 4).ToUInt32();
            Field0x7C = data.Slice(0x7C, 4).ToUInt32();
            Field0x80 = data.Slice(0x80, 4).ToUInt32();
            Field0x84 = data.Slice(0x84, 4).ToUInt32();
            Field0x88 = data.Slice(0x88, 4).ToUInt32();
            Field0x8C = data.Slice(0x8C, 4).ToUInt32();
            Field0x90 = data.Slice(0x90, 4).ToUInt32();
            Field0x94 = data.Slice(0x94, 4).ToUInt32();
            Field0x98 = data.Slice(0x98, 4).ToUInt32();
            LayerScroll = data.Slice(0x9C, 4).ToUInt32();
            data.Slice(0xA0, Field0xA0_0xDC.Length).CopyTo(Field0xA0_0xDC);
        }

        public void Dump(Span<byte> data)
        {
            SceneIndex.ToBytes().CopyTo(data);
            ((uint)Flag).ToBytes().CopyTo(data.Slice(4));
            Name.ToSjisBytes(MaxNameLength).CopyTo(data.Slice(8, MaxNameLength));
            Height.ToBytes().CopyTo(data.Slice(0x48));
            Width.ToBytes().CopyTo(data.Slice(0x4C));
            Field0x50.ToBytes().CopyTo(data.Slice(0x50));
            Cursor.ToBytes().CopyTo(data.Slice(0x54));
            Zoom.ToBytes().CopyTo(data.Slice(0x58));
            TimeScroll.ToBytes().CopyTo(data.Slice(0x5C));
            Field0x60.ToBytes().CopyTo(data.Slice(0x60));
            Field0x64.ToBytes().CopyTo(data.Slice(0x64));
            Field0x68.ToBytes().CopyTo(data.Slice(0x68));
            Field0x6C.ToBytes().CopyTo(data.Slice(0x6C));
            Field0x70.ToBytes().CopyTo(data.Slice(0x70));
            Field0x74.ToBytes().CopyTo(data.Slice(0x74));
            Field0x78.ToBytes().CopyTo(data.Slice(0x78));
            Field0x7C.ToBytes().CopyTo(data.Slice(0x7C));
            Field0x80.ToBytes().CopyTo(data.Slice(0x80));
            Field0x84.ToBytes().CopyTo(data.Slice(0x84));
            Field0x88.ToBytes().CopyTo(data.Slice(0x88));
            Field0x8C.ToBytes().CopyTo(data.Slice(0x8C));
            Field0x90.ToBytes().CopyTo(data.Slice(0x90));
            Field0x94.ToBytes().CopyTo(data.Slice(0x94));
            Field0x98.ToBytes().CopyTo(data.Slice(0x98));
            LayerScroll.ToBytes().CopyTo(data.Slice(0x9C));
            Field0xA0_0xDC.CopyTo(data.Slice(0xA0));
        }
    }
}
