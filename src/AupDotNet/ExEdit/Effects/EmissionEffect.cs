using System;
using System.Drawing;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 発光
    /// </summary>
    public class EmissionEffect : Effect
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

        /// <summary>サイズ固定</summary>
        public bool FixSize
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>光色の設定</summary>
        public Color Color { get; set; }

        /// <summary>光色の設定(色指定なし)</summary>
        public bool NoColor { get; set; }

        public EmissionEffect()
            : base(EffectType)
        {
        }

        public EmissionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public EmissionEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
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

        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("color=");
            writer.WriteLine(ExeditUtil.ColorToString(Color));
            writer.Write("no_color=");
            writer.WriteLine(NoColor ? '1' : '0');
        }

        static EmissionEffect()
        {
            EffectType = new EffectType(
                23, 0x04000420, 4, 2, 4, "発光",
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
                    new CheckboxDefinition("サイズ固定", true, 0),
                }
            );
        }
    }
}
