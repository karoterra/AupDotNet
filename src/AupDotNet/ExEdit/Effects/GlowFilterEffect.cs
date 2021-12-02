using System;
using System.Drawing;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// グロー(フィルタオブジェクト)
    /// </summary>
    public class GlowFilterEffect : Effect
    {
        public static EffectType EffectType { get; }

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        /// <summary>拡散</summary>
        public Trackbar Diffusion => Trackbars[1];

        /// <summary>しきい値</summary>
        public Trackbar Threshold => Trackbars[2];

        /// <summary>ぼかし</summary>
        public Trackbar Blur => Trackbars[3];

        /// <summary>光色の設定</summary>
        public Color Color { get; set; }

        /// <summary>光色の設定(色指定なし)</summary>
        public bool NoColor { get; set; }

        /// <summary>形状</summary>
        public int ShapeType { get; set; }

        public GlowFilterEffect()
            : base(EffectType)
        {
        }

        public GlowFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public GlowFilterEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType, trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    Color = span.ToColor();
                    NoColor = span[3] != 0;
                    ShapeType = span.Slice(4, 4).ToInt32();
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
            ShapeType.ToBytes().CopyTo(data, 4);
            return data;
        }

        static GlowFilterEffect()
        {
            EffectType = new EffectType(
                29, 0x04000400, 4, 2, 8, "グロー",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("強さ", 10, 0, 4000, 400),
                    new TrackbarDefinition("拡散", 1, 0, 200, 30),
                    new TrackbarDefinition("しきい値", 10, 0, 2000, 400),
                    new TrackbarDefinition("ぼかし", 1, 0, 50, 1),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("通常", false, 0),
                    new CheckboxDefinition("光色の設定", false, 0),
                }
            );
        }
    }
}
