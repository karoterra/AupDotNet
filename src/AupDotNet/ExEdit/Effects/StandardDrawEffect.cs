using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 標準描画
    /// </summary>
    public class StandardDrawEffect : Effect
    {
        public static EffectType EffectType { get; }

        public Trackbar X => Trackbars[0];
        public Trackbar Y => Trackbars[1];
        public Trackbar Z => Trackbars[2];
        public Trackbar Zoom => Trackbars[3];
        public Trackbar Alpha => Trackbars[4];
        public Trackbar Rotate => Trackbars[5];

        public BlendMode BlendMode { get; set; }

        public StandardDrawEffect()
            : base(EffectType)
        {
        }

        public StandardDrawEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public StandardDrawEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            BlendMode = (BlendMode)data.Slice(0, 4).ToInt32();
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            ((int)BlendMode).ToBytes().CopyTo(data, 0);
            return data;
        }

        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("blend=");
            writer.WriteLine((int)BlendMode);
        }

        static StandardDrawEffect()
        {
            EffectType = new EffectType(
                10, 0x440004D0, 6, 1, 4, "標準描画",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("X", 10, -999999, 999999, 0),
                    new TrackbarDefinition("Y", 10, -999999, 999999, 0),
                    new TrackbarDefinition("Z", 10, -999999, 999999, 0),
                    new TrackbarDefinition("拡大率", 100, 0, 500000, 10000),
                    new TrackbarDefinition("透明度", 10, 0, 1000, 0),
                    new TrackbarDefinition("回転", 100, -360000, 360000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("通常", false, 0),
                }
            );
        }
    }
}
