using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 部分フィルタ(フィルタオブジェクト)
    /// </summary>
    public class PartialFilterEffect : Effect
    {
        public readonly int MaxNameLength = 256;
        public static EffectType EffectType { get; }

        public Trackbar X => Trackbars[0];
        public Trackbar Y => Trackbars[1];
        public Trackbar Rotate => Trackbars[2];
        public Trackbar Size => Trackbars[3];
        public Trackbar AspectRatio => Trackbars[4];
        public Trackbar Blur => Trackbars[5];

        /// <summary>マスクの反転</summary>
        public bool Invert
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        public FigureType FigureType { get; set; } = FigureType.Square;

        public string _name = "";
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

        public FigureNameType NameType
        {
            get
            {
                if (string.IsNullOrEmpty(Name)) return FigureNameType.BuiltIn;
                else if (Name[0] == '*') return FigureNameType.File;
                else return FigureNameType.Figure;
            }
        }

        public string Filename
        {
            get => NameType == FigureNameType.File ? Name.Substring(1) : null;
            set => Name = $"*{value}";
        }

        public PartialFilterEffect()
            : base(EffectType)
        {
        }

        public PartialFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public PartialFilterEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            FigureType = (FigureType)data.Slice(0, 4).ToInt32();
            Name = data.Slice(4, MaxNameLength).ToCleanSjisString();
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            ((int)FigureType).ToBytes().CopyTo(data, 0);
            Name.ToSjisBytes(MaxNameLength).CopyTo(data, 4);
            return data;
        }

        static PartialFilterEffect()
        {
            EffectType = new EffectType(
                88, 0x44000500, 6, 2, 260, "部分フィルタ",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("X", 10, -40000, 40000, 0),
                    new TrackbarDefinition("Y", 10, -40000, 40000, 0),
                    new TrackbarDefinition("回転", 100, -360000, 360000, 0),
                    new TrackbarDefinition("サイズ", 1, 0, 4000, 100),
                    new TrackbarDefinition("縦横比", 10, -1000, 1000, 0),
                    new TrackbarDefinition("ぼかし", 1, 0, 1000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("背景", false, 0),
                    new CheckboxDefinition("マスクの反転", true, 0),
                }
            );
        }
    }
}
