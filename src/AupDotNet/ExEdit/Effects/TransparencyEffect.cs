namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 透明度(基本効果)
    /// </summary>
    public class TransparencyEffect : NoExtDataEffect
    {
        public static EffectType EffectType { get; }

        /// <summary>透明度</summary>
        public Trackbar Transparency => Trackbars[0];

        public TransparencyEffect()
            : base(EffectType)
        {
        }

        public TransparencyEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static TransparencyEffect()
        {
            EffectType = new EffectType(
                53, 0x04008020, 1, 0, 0, "透明度",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("透明度", 10, 0, 1000, 0),
                },
                new CheckboxDefinition[] {}
            );
        }
    }
}
