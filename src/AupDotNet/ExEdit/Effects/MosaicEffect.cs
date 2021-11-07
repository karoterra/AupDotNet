namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// モザイク
    /// </summary>
    public class MosaicEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.Mosaic;

        /// <summary>サイズ</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>タイル風</summary>
        public bool Tile
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public MosaicEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public MosaicEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
