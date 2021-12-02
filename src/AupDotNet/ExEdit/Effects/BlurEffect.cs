namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ぼかし
    /// </summary>
    public class BlurEffect : Effect
    {
        public static EffectType EffectType { get; }

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>縦横比</summary>
        public Trackbar AspectRatio => Trackbars[1];

        /// <summary>光の強さ</summary>
        public Trackbar Intensity => Trackbars[2];

        /// <summary>サイズ固定</summary>
        public bool FixSize
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public BlurEffect()
            : base(EffectType)
        {
        }

        public BlurEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static BlurEffect()
        {
            EffectType = new EffectType(
                18, 0x04000020, 3, 1, 0, "ぼかし",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("範囲", 1, 0, 1000, 5),
                    new TrackbarDefinition("縦横比", 10, -1000, 1000, 0),
                    new TrackbarDefinition("光の強さ", 1, 0, 60, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("サイズ固定", true, 0),
                }
            );
        }
    }
}
