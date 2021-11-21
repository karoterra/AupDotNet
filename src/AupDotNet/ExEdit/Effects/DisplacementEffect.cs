using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ディスプレイスメントマップ
    /// </summary>
    public class DisplacementEffect : Effect
    {
        public readonly int MaxNameLength = 256;
        private const int Id = (int)EffectTypeId.Displacement;

        /// <summary>変形X,拡大変形,回転変形</summary>
        public Trackbar Param0 => Trackbars[0];

        /// <summary>変形Y,拡大縦横,----</summary>
        public Trackbar Param1 => Trackbars[1];

        /// <summary>X</summary>
        public Trackbar X => Trackbars[2];

        /// <summary>Y</summary>
        public Trackbar Y => Trackbars[3];

        /// <summary>回転</summary>
        public Trackbar Rotate => Trackbars[4];

        /// <summary>サイズ</summary>
        public Trackbar Size => Trackbars[5];

        /// <summary>縦横比</summary>
        public Trackbar AspectRatio => Trackbars[6];

        /// <summary>ぼかし</summary>
        public Trackbar Blur => Trackbars[7];

        /// <summary>元のサイズに合わせる</summary>
        public bool Fit
        {
            get => Checkboxes[2] != 0;
            set => Checkboxes[2] = value ? 1 : 0;
        }

        public FigureType FigureType { get; set; } = FigureType.Circle;

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

        /// <summary>変形方法</summary>
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
                    Name = span.Slice(4, MaxNameLength).ToCleanSjisString();
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
            Name.ToSjisBytes(MaxNameLength).CopyTo(data, 4);
            Mode.ToBytes().CopyTo(data, 0x104);
            ((int)Calc).ToBytes().CopyTo(data, 0x108);
            return data;
        }
    }
}
