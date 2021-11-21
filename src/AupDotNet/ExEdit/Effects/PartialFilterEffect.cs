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
        private const int Id = (int)EffectTypeId.PartialFilter;

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
            : base(EffectType.Defaults[Id])
        {
        }

        public PartialFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public PartialFilterEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    FigureType = (FigureType)span.Slice(0, 4).ToInt32();
                    Name = span.Slice(4, MaxNameLength).ToCleanSjisString();
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
            return data;
        }
    }
}
