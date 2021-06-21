using System;
using System.Drawing;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public class TextEffect : Effect
    {
        public readonly int MaxFontLength = 32;
        public readonly int MaxTextLength = 2048;
        private const int Id = (int)EffectTypeId.Text;

        public Trackbar Size => Trackbars[0];
        public Trackbar Speed => Trackbars[1];

        public bool AutoScroll
        {
            get => Checkboxes[2] != 0;
            set => Checkboxes[2] = value ? 1 : 0;
        }

        public bool Split
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public bool DisplayOnMoving
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        public bool Bold
        {
            get => Checkboxes[3] != 0;
            set => Checkboxes[3] = value ? 1 : 0;
        }

        public bool Italic
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        public TextType TextType { get; set; }
        public bool AutoAdjust { get; set; }
        public bool Soft { get; set; } = true;
        public bool Monospace { get; set; }
        public TextAlign Align { get; set; }
        public int SpacingX { get; set; }
        public int SpacingY { get; set; }
        public bool Precision { get; set; } = true;
        public Color Color { get; set; }
        public Color Color2 { get; set; }

        public string _font = "MS UI Gothic";
        public string Font
        {
            get => _font;
            set
            {
                if (value.GetSjisByteCount() >= MaxFontLength)
                {
                    throw new MaxByteCountOfStringException(nameof(Font), MaxFontLength);
                }
                _font = value;
            }
        }

        private string _text = "";
        public string Text
        {
            get => _text;
            set
            {
                if(value.GetUTF16ByteCount() >= MaxTextLength)
                {
                    throw new MaxByteCountOfStringException(nameof(Text), MaxTextLength);
                }
                _text = value;
            }
        }

        public TextEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public TextEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public TextEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    TextType = (TextType)span[0];
                    AutoAdjust = span[1] != 0;
                    Soft = span[2] != 0;
                    Monospace = span[3] != 0;
                    Align = (TextAlign)span[4];
                    SpacingX = unchecked((sbyte)span[5]);
                    SpacingY = unchecked((sbyte)span[6]);
                    Precision = span[7] != 0;
                    Color = span.Slice(8, 4).ToColor();
                    Color2 = span.Slice(0xC, 4).ToColor();
                    Font = span.Slice(0x10, MaxFontLength).ToCleanSjisString();
                    Text = span.Slice(0x30, MaxTextLength).ToCleanUTF16String();
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
            data[0] = (byte)TextType;
            data[1] = (byte)(AutoAdjust ? 1 : 0);
            data[2] = (byte)(Soft ? 1 : 0);
            data[3] = (byte)(Monospace ? 1 : 0);
            data[4] = (byte)Align;
            data[5] = unchecked((byte)SpacingX);
            data[6] = unchecked((byte)SpacingY);
            data[7] = (byte)(Precision ? 1 : 0);
            Color.ToBytes().CopyTo(data, 8);
            Color2.ToBytes().CopyTo(data, 0xC);
            Font.ToSjisBytes(MaxFontLength).CopyTo(data, 0x10);
            Text.ToUTF16Bytes(MaxTextLength).CopyTo(data, 0x30);
            return data;
        }
    }
}
