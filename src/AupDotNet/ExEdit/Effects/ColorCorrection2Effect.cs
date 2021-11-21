namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 色調補正(AviUtl組込みフィルタ)
    /// </summary>
    public class ColorCorrection2Effect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.ColorCorrection2;

        /// <summary>明るさ</summary>
        public Trackbar Brightness => Trackbars[0];

        /// <summary>コントラスト</summary>
        public Trackbar Contrast => Trackbars[1];

        /// <summary>ガンマ</summary>
        public Trackbar Gamma => Trackbars[2];

        /// <summary>輝度</summary>
        public Trackbar Luminance => Trackbars[3];

        /// <summary>色の濃さ</summary>
        public Trackbar Depth => Trackbars[4];

        /// <summary>色合い</summary>
        public Trackbar Tone => Trackbars[5];

        public ColorCorrection2Effect()
            : base(EffectType.Defaults[Id])
        {
        }

        public ColorCorrection2Effect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
