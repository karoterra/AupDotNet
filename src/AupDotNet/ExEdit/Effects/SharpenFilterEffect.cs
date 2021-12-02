namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// シャープフィルタ(AviUtl組込みフィルタ)
    /// </summary>
    public class SharpenFilterEffect : NoExtDataEffect
    {
        public static EffectType EffectType { get; }

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[1];

        /// <summary>下限値</summary>
        public Trackbar Lower => Trackbars[2];

        /// <summary>上限値</summary>
        public Trackbar Upper => Trackbars[3];

        public SharpenFilterEffect()
            : base(EffectType)
        {
        }

        public SharpenFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static SharpenFilterEffect()
        {
            EffectType = new EffectType(
                101, 0x02000000, 4, 0, 0, "シャープフィルタ",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("強さ", 1, 0, 256, 256),
                    new TrackbarDefinition("範囲", 1, 1, 3, 2),
                    new TrackbarDefinition("下限値", 1, 0, 1024, 128),
                    new TrackbarDefinition("上限値", 1, 0, 1024, 1024),
                },
                new CheckboxDefinition[] { }
            );
        }
    }
}
