using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// クロマキー
    /// </summary>
    public class ChromaKeyEffect : Effect
    {
        public static EffectType EffectType { get; }

        /// <summary>色相範囲</summary>
        public Trackbar HueRange => Trackbars[0];

        /// <summary>彩度範囲</summary>
        public Trackbar ChromaRange => Trackbars[1];

        /// <summary>境界補正</summary>
        public Trackbar Boundary => Trackbars[2];

        /// <summary>色彩補正</summary>
        public bool ColorCorrection
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>透過補正</summary>
        public bool TransparencyCorrection
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>キー色の取得</summary>
        public YCbCr Color { get; set; }

        /// <summary>キー(未取得)</summary>
        public int Status { get; set; }

        public short Field0x6 { get; set; }

        public ChromaKeyEffect()
            : base(EffectType)
        {
        }

        public ChromaKeyEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public ChromaKeyEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Color = data.ToYCbCr();
            Field0x6 = data.Slice(6, 2).ToInt16();
            Status = data.Slice(8, 4).ToInt32();
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Color.ToBytes().CopyTo(data, 0);
            Field0x6.ToBytes().CopyTo(data, 6);
            Status.ToBytes().CopyTo(data, 8);
            return data;
        }

        static ChromaKeyEffect()
        {
            EffectType = new EffectType(
                30, 0x04000420, 3, 3, 12, "クロマキー",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("色相範囲", 1, 0, 256, 24),
                    new TrackbarDefinition("彩度範囲", 1, 0, 256, 96),
                    new TrackbarDefinition("境界補正", 1, 0, 5, 1),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("色彩補正", true, 0),
                    new CheckboxDefinition("透過補正", true, 0),
                    new CheckboxDefinition("キー色の取得", false, 0),
                }
            );
        }
    }
}
