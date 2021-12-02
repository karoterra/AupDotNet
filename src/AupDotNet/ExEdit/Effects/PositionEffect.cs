namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 座標(基本効果)
    /// </summary>
    public class PositionEffect : NoExtDataEffect
    {
        public static EffectType EffectType { get; }

        public Trackbar X => Trackbars[0];
        public Trackbar Y => Trackbars[1];
        public Trackbar Z => Trackbars[2];

        public PositionEffect()
            : base(EffectType)
        {
        }

        public PositionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static PositionEffect()
        {
            EffectType = new EffectType(
                51, 0x04008020, 3, 0, 0, "座標",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("X", 10, -999999, 999999, 0),
                    new TrackbarDefinition("Y", 10, -999999, 999999, 0),
                    new TrackbarDefinition("Z", 10, -999999, 999999, 0),
                },
                new CheckboxDefinition[] {}
            );
        }
    }
}
