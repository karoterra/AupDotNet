using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public class VideoFileEffect : Effect
    {
        public readonly int MaxFilenameLength = 256;
        private const int Id = (int)EffectTypeId.VideoFile;

        public Trackbar Position => Trackbars[0];
        public Trackbar Speed => Trackbars[1];

        public bool Loop
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public bool Alpha
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        private string _filename = "";
        public string Filename
        {
            get => _filename;
            set
            {
                if (value.GetSjisByteCount() >= MaxFilenameLength)
                {
                    throw new MaxByteCountOfStringException(nameof(Filename), MaxFilenameLength);
                }
                _filename = value;
            }
        }

        public uint Field0x100 { get; set; }
        public uint Field0x104 { get; set; }
        public uint Field0x108 { get; set; }
        public uint Field0x10C { get; set; }
        public uint Field0x110 { get; set; }
        public uint Field0x114 { get; set; }
        public uint Field0x118 { get; set; }

        public VideoFileEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public VideoFileEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public VideoFileEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    Filename = span.Slice(0, MaxFilenameLength).ToCleanSjisString();
                    Field0x100 = span.Slice(0x100, 4).ToUInt32();
                    Field0x104 = span.Slice(0x104, 4).ToUInt32();
                    Field0x108 = span.Slice(0x108, 4).ToUInt32();
                    Field0x10C = span.Slice(0x10C, 4).ToUInt32();
                    Field0x110 = span.Slice(0x110, 4).ToUInt32();
                    Field0x114 = span.Slice(0x114, 4).ToUInt32();
                    Field0x118 = span.Slice(0x118, 4).ToUInt32();
                }
                else if (data.Length != 0)
                {
                    throw new ArgumentException("data's length is invalid.");
                }
            }
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Filename.ToSjisBytes(MaxFilenameLength).CopyTo(data, 0);
            Field0x100.ToBytes().CopyTo(data, 0x100);
            Field0x104.ToBytes().CopyTo(data, 0x104);
            Field0x108.ToBytes().CopyTo(data, 0x108);
            Field0x10C.ToBytes().CopyTo(data, 0x10C);
            Field0x110.ToBytes().CopyTo(data, 0x110);
            Field0x114.ToBytes().CopyTo(data, 0x114);
            Field0x118.ToBytes().CopyTo(data, 0x118);
            return data;
        }
    }
}
