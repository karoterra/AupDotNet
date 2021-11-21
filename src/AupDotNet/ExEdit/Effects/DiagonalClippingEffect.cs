namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 斜めクリッピング
    /// </summary>
    public class DiagonalClippingEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.DiagonalClipping;

        /// <summary>中心X</summary>
        public Trackbar X => Trackbars[0];

        /// <summary>中心Y</summary>
        public Trackbar Y => Trackbars[1];

        /// <summary>角度</summary>
        public Trackbar Angle => Trackbars[2];

        /// <summary>ぼかし</summary>
        public Trackbar Blur => Trackbars[3];

        /// <summary>幅</summary>
        public Trackbar Width => Trackbars[4];

        public DiagonalClippingEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public DiagonalClippingEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
