using System;
using System.Drawing;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 図形
    /// </summary>
    public class FigureEffect : Effect
    {
        public readonly int MaxNameLength = 256;
        public static EffectType EffectType { get; }

        /// <summary>サイズ</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>縦横比</summary>
        public Trackbar AspectRatio => Trackbars[1];

        /// <summary>ライン幅</summary>
        public Trackbar LineWidth => Trackbars[2];

        public FigureType FigureType { get; set; } = FigureType.Circle;
        public Color Color { get; set; } = Color.White;

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

        public FigureEffect()
            : base(EffectType)
        {
        }

        public FigureEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public FigureEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            FigureType = (FigureType)data.Slice(0, 4).ToInt32();
            Color = data.Slice(4, 4).ToColor();
            Name = data.Slice(8, MaxNameLength).ToCleanSjisString();
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            ((int)FigureType).ToBytes().CopyTo(data, 0);
            Color.ToBytes().CopyTo(data, 4);
            Name.ToSjisBytes(MaxNameLength).CopyTo(data, 8);
            return data;
        }

        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("type=");
            writer.WriteLine((int)FigureType);
            writer.Write("color=");
            writer.WriteLine(ExeditUtil.ColorToString(Color));
            writer.Write("name=");
            writer.WriteLine(Name);
        }

        static FigureEffect()
        {
            EffectType = new EffectType(
                4, 0x04000408, 3, 2, 264, "図形",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("サイズ", 1, 0, 4000, 100),
                    new TrackbarDefinition("縦横比", 10, -1000, 1000, 0),
                    new TrackbarDefinition("ライン幅", 1, 0, 4000, 4000),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("背景", false, 0),
                    new CheckboxDefinition("色の設定", false, 0),
                }
            );
        }
    }
}
