using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public class MaskEffect : Effect
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

        /// <summary>元のサイズに合わせる</summary>
        public bool Fit
        {
            get => Checkboxes[2] != 0;
            set => Checkboxes[2] = value ? 1 : 0;
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
                else if (Name[0] == ':') return FigureNameType.Scene;
                else return FigureNameType.Figure;
            }
        }

        public string Filename
        {
            get => NameType == FigureNameType.File ? Name.Substring(1) : null;
            set => Name = $"*{value}";
        }

        public int? Scene
        {
            get
            {
                if (NameType != FigureNameType.Scene) return null;
                bool success = int.TryParse(Name.Substring(1), out int scene);
                if (success) return scene;
                else return null;
            }
            set
            {
                if (!value.HasValue) value = 0;
                Name = $":{value}";
            }
        }

        /// <summary>
        /// シーン選択時に有効。
        /// 1: シーンの長さを合わせる
        /// 2: シーンを逆再生
        /// </summary>
        public int Mode { get; set; }

        public MaskEffect()
            : base(EffectType)
        {
        }

        public MaskEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public MaskEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType, trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    FigureType = (FigureType)span.Slice(0, 4).ToInt32();
                    Name = span.Slice(4, MaxNameLength).ToCleanSjisString();
                    Mode = span.Slice(0x104, 4).ToInt32();
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
            Name.ToSjisBytes(MaxNameLength).CopyTo(data, 4);
            Mode.ToBytes().CopyTo(data, 0x104);
            return data;
        }

        static MaskEffect()
        {
            EffectType = new EffectType(
                41, 0x04000620, 6, 3, 264, "マスク",
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
                    new CheckboxDefinition("元のサイズに合わせる", true, 0),
                }
            );
        }
    }
}
