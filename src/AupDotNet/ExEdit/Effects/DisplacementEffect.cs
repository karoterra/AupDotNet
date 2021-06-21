using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public class DisplacementEffect : Effect
    {
        public readonly int MaxFilenameLength = 256;
        private const int Id = (int)EffectTypeId.Displacement;

        public Trackbar Param0 => Trackbars[0];
        public Trackbar Param1 => Trackbars[1];
        public Trackbar X => Trackbars[2];
        public Trackbar Y => Trackbars[3];
        public Trackbar Rotate => Trackbars[4];
        public Trackbar Size => Trackbars[5];
        public Trackbar AspectRatio => Trackbars[6];
        public Trackbar Blur => Trackbars[7];

        public bool Fit
        {
            get => Checkboxes[2] != 0;
            set => Checkboxes[2] = value ? 1 : 0;
        }

        public FigureType FigureType { get; set; } = FigureType.Circle;

        public string _filename = "";
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

        public int Mode { get; set; }

        public DisplacementCalc Calc { get; set; }

        public DisplacementEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public DisplacementEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public DisplacementEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    FigureType = (FigureType)span.Slice(0, 4).ToInt32();
                    Filename = span.Slice(4, MaxFilenameLength).ToCleanSjisString();
                    Mode = span.Slice(0x104, 4).ToInt32();
                    Calc = (DisplacementCalc)span.Slice(0x108, 4).ToInt32();
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
            ((int)FigureType).ToBytes().CopyTo(data, 0);
            Filename.ToSjisBytes(MaxFilenameLength).CopyTo(data, 4);
            Mode.ToBytes().CopyTo(data, 0x104);
            ((int)Calc).ToBytes().CopyTo(data, 0x108);
            return data;
        }
    }
}
