namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ローテーション(基本効果)
    /// </summary>
    public class Rotation90Effect : Effect
    {
        public static EffectType EffectType { get; }

        /// <summary>90度回転</summary>
        public Trackbar Rotation => Trackbars[0];

        public Rotation90Effect()
            : base(EffectType)
        {
        }

        public Rotation90Effect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static Rotation90Effect()
        {
            EffectType = new EffectType(
                57, 0x04008020, 1, 0, 0, "ローテーション",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("90度回転", 1, -4, 4, 0),
                },
                new CheckboxDefinition[] { }
            );
        }
    }
}
