namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 境界ぼかし
    /// </summary>
    public class BorderBlurEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.BorderBlur;

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>縦横比</summary>
        public Trackbar AspectRatio => Trackbars[1];

        /// <summary>透明度の境界をぼかす</summary>
        public bool BlurAlpha
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public BorderBlurEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public BorderBlurEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
