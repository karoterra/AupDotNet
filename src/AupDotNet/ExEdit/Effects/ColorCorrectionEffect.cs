namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 色調補正
    /// </summary>
    public class ColorCorrectionEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.ColorCorrection;

        /// <summary>明るさ</summary>
        public Trackbar Brightness => Trackbars[0];

        /// <summary>コントラスト</summary>
        public Trackbar Contrast => Trackbars[1];

        /// <summary>色相</summary>
        public Trackbar Hue => Trackbars[2];

        /// <summary>輝度</summary>
        public Trackbar Luminance => Trackbars[3];

        /// <summary>彩度</summary>
        public Trackbar Chroma => Trackbars[4];

        /// <summary>飽和する</summary>
        public bool Saturation
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public ColorCorrectionEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public ColorCorrectionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
