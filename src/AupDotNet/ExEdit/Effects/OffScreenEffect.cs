namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// オフスクリーン描画
    /// </summary>
    public class OffScreenEffect : Effect
    {
        public static EffectType EffectType { get; }

        public OffScreenEffect()
            : base(EffectType)
        {
        }

        public OffScreenEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static OffScreenEffect()
        {
            EffectType = new EffectType(
                86, 0x04000020, 0, 0, 0, "オフスクリーン描画",
                new TrackbarDefinition[] { },
                new CheckboxDefinition[] { }
            );
        }
    }
}
