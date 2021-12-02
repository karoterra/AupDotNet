namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// レンズブラー(フィルタオブジェクト)
    /// </summary>
    public class LensBlurFilterEffect : Effect
    {
        public static EffectType EffectType { get; }

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>光の強さ</summary>
        public Trackbar Intensity => Trackbars[1];

        public LensBlurFilterEffect()
            : base(EffectType)
        {
        }

        public LensBlurFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static LensBlurFilterEffect()
        {
            EffectType = new EffectType(
                48, 0x04000000, 2, 0, 0, "レンズブラー",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("範囲", 1, 0, 1000, 5),
                    new TrackbarDefinition("光の強さ", 1, 0, 60, 32),
                },
                new CheckboxDefinition[] { }
            );
        }
    }
}
