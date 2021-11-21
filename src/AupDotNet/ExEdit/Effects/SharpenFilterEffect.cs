namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// シャープフィルタ(AviUtl組込みフィルタ)
    /// </summary>
    public class SharpenFilterEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.SharpenFilter;

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[1];

        /// <summary>下限値</summary>
        public Trackbar Lower => Trackbars[2];

        /// <summary>上限値</summary>
        public Trackbar Upper => Trackbars[3];

        public SharpenFilterEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public SharpenFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
