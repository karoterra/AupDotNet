namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 回転(基本効果)
    /// </summary>
    public class RotationEffect : NoExtDataEffect
    {
        public static EffectType EffectType { get; }

        public Trackbar X => Trackbars[0];
        public Trackbar Y => Trackbars[1];
        public Trackbar Z => Trackbars[2];

        public RotationEffect()
            : base(EffectType)
        {
        }

        public RotationEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static RotationEffect()
        {
            EffectType = new EffectType(
                54, 0x04008020, 3, 0, 0, "回転",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("X", 100, -360000, 360000, 0),
                    new TrackbarDefinition("Y", 100, -360000, 360000, 0),
                    new TrackbarDefinition("Z", 100, -360000, 360000, 0),
                },
                new CheckboxDefinition[] { }
            );
        }
    }
}
