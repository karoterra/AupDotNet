namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 領域拡張(基本効果)
    /// </summary>
    public class AreaExpansionEffect : Effect
    {
        public static EffectType EffectType { get; }

        public Trackbar Top => Trackbars[0];
        public Trackbar Bottom => Trackbars[1];
        public Trackbar Left => Trackbars[2];
        public Trackbar Right => Trackbars[3];

        /// <summary>塗りつぶし</summary>
        public bool Fill
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public AreaExpansionEffect()
            : base(EffectType)
        {
        }

        public AreaExpansionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static AreaExpansionEffect()
        {
            EffectType = new EffectType(
                55, 0x04008020, 4, 1, 0, "領域拡張",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("上", 1, 0, 4000, 0),
                    new TrackbarDefinition("下", 1, 0, 4000, 0),
                    new TrackbarDefinition("左", 1, 0, 4000, 0),
                    new TrackbarDefinition("右", 1, 0, 4000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("塗りつぶし", true, 0),
                }
            );
        }
    }
}
