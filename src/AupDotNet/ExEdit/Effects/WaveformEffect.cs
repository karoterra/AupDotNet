using System;
using System.Drawing;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public class WaveformEffect : Effect
    {
        public readonly int MaxFilenameLength = 256;
        private const int Id = (int)EffectTypeId.Waveform;

        public Trackbar Width => Trackbars[0];
        public Trackbar Height => Trackbars[1];
        public Trackbar Volume => Trackbars[2];
        public Trackbar Position => Trackbars[3];

        public bool ReferScene
        {
            get => Checkboxes[3] != 0;
            set => Checkboxes[3] = value ? 1 : 0;
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

        public byte[] Field0x104 { get; set; } = new byte[20];

        public short PresetType { get; set; }

        public short Mode { get; set; }
        public short ResW { get; set; }
        public short ResH { get; set; }
        public short PadW { get; set; }
        public short PadH { get; set; }
        public Color Color { get; set; }
        public int SampleN { get; set; }
        public bool Mirror { get; set; }

        public WaveformEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public WaveformEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public WaveformEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    Filename = span.Slice(0, MaxFilenameLength).ToCleanSjisString();
                    Field0x104 = span.Slice(0x104, 20).ToArray();
                    PresetType = span.Slice(0x118, 2).ToInt16();
                    Mode = span.Slice(0x11A, 2).ToInt16();
                    ResW = span.Slice(0x11C, 2).ToInt16();
                    ResH = span.Slice(0x11E, 2).ToInt16();
                    PadW = span.Slice(0x120, 2).ToInt16();
                    PadH = span.Slice(0x122, 2).ToInt16();
                    Color = span.Slice(0x124, 4).ToColor();
                    SampleN = span.Slice(0x128, 4).ToInt32();
                    Mirror = span[0x12C] != 0;
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
            Field0x104.CopyTo(data, 0x104);
            PresetType.ToBytes().CopyTo(data, 0x118);
            Mode.ToBytes().CopyTo(data, 0x11A);
            ResW.ToBytes().CopyTo(data, 0x11C);
            ResH.ToBytes().CopyTo(data, 0x11E);
            PadW.ToBytes().CopyTo(data, 0x120);
            PadH.ToBytes().CopyTo(data, 0x122);
            Color.ToBytes().CopyTo(data, 0x124);
            SampleN.ToBytes().CopyTo(data, 0x128);
            data[0x12C] = Mirror ? (byte)1 : (byte)0;
            return data;
        }
    }
}
