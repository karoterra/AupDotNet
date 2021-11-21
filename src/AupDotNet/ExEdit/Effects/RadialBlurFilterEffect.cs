namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 放射ブラー(フィルタオブジェクト)
    /// </summary>
    public class RadialBlurFilterEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.RadialBlurFilter;

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>中心X</summary>
        public Trackbar X => Trackbars[1];

        /// <summary>中心Y</summary>
        public Trackbar Y => Trackbars[2];

        public RadialBlurFilterEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public RadialBlurFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
