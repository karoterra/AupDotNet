namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ぼかし(フィルタオブジェクト)
    /// </summary>
    public class BlurFilterEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.BlurFilter;

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>縦横比</summary>
        public Trackbar AspectRatio => Trackbars[1];

        /// <summary>光の強さ</summary>
        public Trackbar Intensity => Trackbars[2];

        public BlurFilterEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public BlurFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
