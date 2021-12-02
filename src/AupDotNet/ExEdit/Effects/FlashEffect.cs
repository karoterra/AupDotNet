using System;
using System.Drawing;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 閃光
    /// </summary>
    public class FlashEffect : Effect
    {
        public static EffectType EffectType { get; }

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        public Trackbar X => Trackbars[1];
        public Trackbar Y => Trackbars[2];

        /// <summary>サイズ固定</summary>
        public bool FixSize
        {
            get => Checkboxes[2] != 0;
            set => Checkboxes[2] = value ? 1 : 0;
        }

        /// <summary>光色の設定</summary>
        public Color Color { get; set; }

        /// <summary>光色の設定(色指定なし)</summary>
        public bool NoColor { get; set; }

        /// <summary>
        /// <list type="bullet">
        ///     <item>0. 前方に合成</item>
        ///     <item>1. 後方に合成</item>
        ///     <item>2. 光成分のみ</item>
        /// </list>
        /// </summary>
        public int Mode { get; set; }

        public FlashEffect()
            : base(EffectType)
        {
        }

        public FlashEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public FlashEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType, trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    Color = span.ToColor();
                    NoColor = span[3] != 0;
                    Mode = span.Slice(4, 4).ToInt32();
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
            data[3] = (byte)(NoColor ? 1 : 0);
            Mode.ToBytes().CopyTo(data, 4);
            return data;
        }

        static FlashEffect()
        {
            EffectType = new EffectType(
                25, 0x04000420, 3, 3, 8, "閃光",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("強さ", 10, 0, 1000, 1000),
                    new TrackbarDefinition("X", 1, -2000, 2000, 0),
                    new TrackbarDefinition("Y", 1, -2000, 2000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("前方に合成", false, 0),
                    new CheckboxDefinition("光色の設定", false, 0),
                    new CheckboxDefinition("サイズ固定", true, 0),
                }
            );
        }
    }
}
