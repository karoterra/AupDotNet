namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// レンズブラー
    /// </summary>
    public class LensBlurEffect : NoExtDataEffect
    {
        public static EffectType EffectType { get; }

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>光の強さ</summary>
        public Trackbar Intensity => Trackbars[1];

        /// <summary>サイズ固定</summary>
        public bool FixSize
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public LensBlurEffect()
            : base(EffectType)
        {
        }

        public LensBlurEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static LensBlurEffect()
        {
            EffectType = new EffectType(
                47, 0x04000020, 2, 1, 0, "レンズブラー",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("範囲", 1, 0, 1000, 5),
                    new TrackbarDefinition("光の強さ", 1, 0, 60, 32),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("サイズ固定", true, 1),
                }
            );
        }
    }
}
