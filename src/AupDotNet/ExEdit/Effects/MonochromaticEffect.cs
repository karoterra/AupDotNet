using System;
using System.Drawing;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 単色化
    /// </summary>
    public class MonochromaticEffect : Effect
    {
        public static EffectType EffectType { get; }

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        /// <summary>輝度を保持する</summary>
        public bool KeepLuminance
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>色の設定</summary>
        public Color Color { get; set; }

        public MonochromaticEffect()
            : base(EffectType)
        {
        }

        public MonochromaticEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public MonochromaticEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType, trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    Color = span.Slice(0, 4).ToColor();
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
            Color.ToBytes().CopyTo(data, 0);
            return data;
        }

        static MonochromaticEffect()
        {
            EffectType = new EffectType(
                73, 0x04000420, 1, 2, 4, "単色化",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("強さ", 10, 0, 1000, 1000),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("色の設定", false, 0),
                    new CheckboxDefinition("輝度を保持する", true, 1),
                }
            );
        }
    }
}
