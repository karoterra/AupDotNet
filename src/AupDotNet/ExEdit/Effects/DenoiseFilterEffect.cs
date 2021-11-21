namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ノイズ除去フィルタ(AviUtl組込みフィルタ)
    /// </summary>
    public class DenoiseFilterEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.DenoiseFilter;

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[1];

        /// <summary>しきい値</summary>
        public Trackbar Threshold => Trackbars[2];

        public DenoiseFilterEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public DenoiseFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
