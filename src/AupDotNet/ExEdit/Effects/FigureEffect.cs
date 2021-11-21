using System;
using System.Drawing;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 図形
    /// </summary>
    public class FigureEffect : Effect
    {
        public readonly int MaxNameLength = 256;
        private const int Id = (int)EffectTypeId.Figure;

        public Trackbar Size => Trackbars[0];
        public Trackbar AspectRatio => Trackbars[1];
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
            : base(EffectType.Defaults[Id])
        {
        }

        public FigureEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public FigureEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    FigureType = (FigureType)span.Slice(0, 4).ToInt32();
                    Color = span.Slice(4, 4).ToColor();
                    Name = span.Slice(8, MaxNameLength).ToCleanSjisString();
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
            Color.ToBytes().CopyTo(data, 4);
            Name.ToSjisBytes(MaxNameLength).CopyTo(data, 8);
            return data;
        }
    }
}
