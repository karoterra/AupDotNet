using System;
using System.Drawing;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 単色化(フィルタオブジェクト)
    /// </summary>
    public class MonochromaticFilterEffect : Effect
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

        public Color Color { get; set; }

        public MonochromaticFilterEffect()
            : base(EffectType)
        {
        }

        public MonochromaticFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public MonochromaticFilterEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Color = data.Slice(0, 4).ToColor();
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Color.ToBytes().CopyTo(data, 0);
            return data;
        }

        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("color=");
            writer.WriteLine(ExeditUtil.ColorToString(Color));
        }

        static MonochromaticFilterEffect()
        {
            EffectType = new EffectType(
                74, 0x04000400, 1, 2, 4, "単色化",
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
