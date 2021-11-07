namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// モザイク(フィルタオブジェクト)
    /// </summary>
    public class MosaicFilterEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.MosaicFilter;

        /// <summary>サイズ</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>タイル風</summary>
        public bool Tile
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public MosaicFilterEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public MosaicFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
