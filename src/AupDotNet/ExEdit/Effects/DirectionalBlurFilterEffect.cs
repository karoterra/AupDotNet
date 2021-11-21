namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 方向ブラー(フィルタオブジェクト)
    /// </summary>
    public class DirectionalBlurFilterEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.DirectionalBlurFilter;

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>角度</summary>
        public Trackbar Angle => Trackbars[1];

        public DirectionalBlurFilterEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public DirectionalBlurFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
