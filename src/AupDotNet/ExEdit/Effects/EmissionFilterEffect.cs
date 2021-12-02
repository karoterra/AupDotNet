using System;
using System.Drawing;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 発光(フィルタオブジェクト)
    /// </summary>
    public class EmissionFilterEffect : Effect
    {
        public static EffectType EffectType { get; }

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        /// <summary>拡散</summary>
        public Trackbar Diffusion => Trackbars[1];

        /// <summary>しきい値</summary>
        public Trackbar Threshold => Trackbars[2];

        /// <summary>拡散速度</summary>
        public Trackbar DiffusionRate => Trackbars[3];

        /// <summary>光色の設定</summary>
        public Color Color { get; set; }

        /// <summary>光色の設定(色指定なし)</summary>
        public bool NoColor { get; set; }

        public EmissionFilterEffect()
            : base(EffectType)
        {
        }

        public EmissionFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public EmissionFilterEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Color = data.ToColor();
            NoColor = data[3] != 0;
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Color.ToBytes().CopyTo(data, 0);
            data[3] = (byte)(NoColor ? 1 : 0);
            return data;
        }

        static EmissionFilterEffect()
        {
            EffectType = new EffectType(
                24, 0x04000400, 4, 1, 4, "発光",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("強さ", 10, 0, 2000, 1000),
                    new TrackbarDefinition("拡散", 1, 10, 2000, 250),
                    new TrackbarDefinition("しきい値", 10, 0, 2000, 800),
                    new TrackbarDefinition("拡散速度", 1, 0, 60, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("光色の設定", false, 0),
                }
            );
        }
    }
}
