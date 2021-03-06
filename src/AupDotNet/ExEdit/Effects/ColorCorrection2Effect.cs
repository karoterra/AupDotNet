namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 色調補正(AviUtl組込みフィルタ)
    /// </summary>
    public class ColorCorrection2Effect : Effect
    {
        /// <summary>
        /// 色調補正のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

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

        /// <summary>
        /// <see cref="ColorCorrection2Effect"/> のインスタンスを初期化します。
        /// </summary>
        public ColorCorrection2Effect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="ColorCorrection2Effect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public ColorCorrection2Effect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static ColorCorrection2Effect()
        {
            EffectType = new EffectType(
                105, 0x02001000, 6, 0, 0, "色調補正",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("明るさ", 1, -256, 256, 0),
                    new TrackbarDefinition("コントラスト", 1, -256, 256, 0),
                    new TrackbarDefinition("ガンマ", 1, -256, 256, 0),
                    new TrackbarDefinition("輝度", 1, -256, 256, 0),
                    new TrackbarDefinition("色の濃さ", 1, -256, 256, 0),
                    new TrackbarDefinition("色合い", 1, -256, 256, 0),
                },
                System.Array.Empty<CheckboxDefinition>()
            );
        }
    }
}
