namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// オブジェクト分割
    /// </summary>
    public class SplitEffect : NoExtDataEffect
    {
        public static EffectType EffectType { get; }

        /// <summary>横分割数</summary>
        public Trackbar Horizontal => Trackbars[0];

        /// <summary>縦分割数</summary>
        public Trackbar Vertical => Trackbars[1];

        public SplitEffect()
            : base(EffectType)
        {
        }

        public SplitEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static SplitEffect()
        {
            EffectType = new EffectType(
                87, 0x04000020, 2, 0, 0, "オブジェクト分割",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("横分割数", 1, 1, 100, 10),
                    new TrackbarDefinition("縦分割数", 1, 1, 100, 10),
                },
                new CheckboxDefinition[] { }
            );
        }
    }
}
