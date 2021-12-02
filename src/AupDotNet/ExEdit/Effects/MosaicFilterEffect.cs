namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// モザイク(フィルタオブジェクト)
    /// </summary>
    public class MosaicFilterEffect : Effect
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

        public MosaicFilterEffect()
            : base(EffectType)
        {
        }

        public MosaicFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static MosaicFilterEffect()
        {
            EffectType = new EffectType(
                22, 0x04000000, 1, 1, 0, "モザイク",
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
