namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 色調補正
    /// </summary>
    public class ColorCorrectionEffect : Effect
    {
        /// <summary>
        /// 色調補正のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

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

        /// <summary>
        /// <see cref="ColorCorrectionEffect"/> のインスタンスを初期化します。
        /// </summary>
        public ColorCorrectionEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="ColorCorrectionEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public ColorCorrectionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static ColorCorrectionEffect()
        {
            EffectType = new EffectType(
                15, 0x04000020, 5, 1, 0, "色調補正",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("明るさ", 10, 0, 2000, 1000),
                    new TrackbarDefinition("ｺﾝﾄﾗｽﾄ", 10, 0, 2000, 1000),
                    new TrackbarDefinition("色相", 10, -36000, 36000, 0),
                    new TrackbarDefinition("輝度", 10, 0, 2000, 1000),
                    new TrackbarDefinition("彩度", 10, 0, 2000, 1000),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("飽和する", true, 0),
                }
            );
        }
    }
}
