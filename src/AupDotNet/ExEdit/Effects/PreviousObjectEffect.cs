using System;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 直前オブジェクト
    /// </summary>
    public class PreviousObjectEffect : Effect
    {
        public static EffectType EffectType { get; }

        public PreviousObjectEffect()
            : base(EffectType)
        {
        }

        public PreviousObjectEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static PreviousObjectEffect()
        {
            EffectType = new EffectType(
                9, 0x04000008, 0, 0, 0, "直前オブジェクト",
                new TrackbarDefinition[] {},
                new CheckboxDefinition[] {}
            );
        }
    }
}
