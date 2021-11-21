namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// レンズブラー(フィルタオブジェクト)
    /// </summary>
    public class LensBlurFilterEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.LensBlurFilter;

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>光の強さ</summary>
        public Trackbar Intensity => Trackbars[1];

        public LensBlurFilterEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public LensBlurFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
