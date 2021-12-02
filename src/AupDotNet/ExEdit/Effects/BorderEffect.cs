using System;
using System.Drawing;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 縁取り
    /// </summary>
    public class BorderEffect : Effect
    {
        public readonly int MaxFilenameLength = 256;
        public static EffectType EffectType { get; }

        /// <summary>サイズ</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>ぼかし</summary>
        public Trackbar Blur => Trackbars[1];

        /// <summary>縁色の設定</summary>
        public Color Color { get; set; } = Color.Black;

        private string _filename = "";
        /// <summary>パターン画像ファイル</summary>
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

        public BorderEffect()
            : base(EffectType)
        {
        }

        public BorderEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public BorderEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Color = data.Slice(0, 4).ToColor(true);
            Filename = data.Slice(4, MaxFilenameLength).ToCleanSjisString();
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Color.ToBytes(true).CopyTo(data, 0);
            Filename.ToSjisBytes(MaxFilenameLength).CopyTo(data, 4);
            return data;
        }

        static BorderEffect()
        {
            EffectType = new EffectType(
                35, 0x44000420, 2, 2, 260, "縁取り",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("サイズ", 1, 0, 500, 3),
                    new TrackbarDefinition("ぼかし", 1, 0, 100, 10),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("縁色の設定", false, 0),
                    new CheckboxDefinition("パターン画像ファイル", false, 0),
                }
            );
        }
    }
}
