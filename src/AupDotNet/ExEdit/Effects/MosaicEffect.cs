namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// モザイク
    /// </summary>
    public class MosaicEffect : Effect
    {
        public static EffectType EffectType { get; }

        /// <summary>サイズ</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>タイル風</summary>
        public bool Tile
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public MosaicEffect()
            : base(EffectType)
        {
        }

        public MosaicEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static MosaicEffect()
        {
            EffectType = new EffectType(
                21, 0x04000020, 1, 1, 0, "モザイク",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("サイズ", 1, 1, 2000, 12),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("タイル風", true, 0),
                }
            );
        }
    }
}
