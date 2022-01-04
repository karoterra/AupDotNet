namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 画像ループ
    /// </summary>
    public class ImageLoopEffect : Effect
    {
        /// <summary>
        /// 画像ループのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>横回数</summary>
        public Trackbar NumX => Trackbars[0];

        /// <summary>縦回数</summary>
        public Trackbar NumY => Trackbars[1];

        /// <summary>速度X</summary>
        public Trackbar SpeedX => Trackbars[2];

        /// <summary>速度Y</summary>
        public Trackbar SpeedY => Trackbars[3];

        /// <summary>個別オブジェクト</summary>
        public bool Split
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>
        /// <see cref="ImageLoopEffect"/> のインスタンスを初期化します。
        /// </summary>
        public ImageLoopEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="ImageLoopEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public ImageLoopEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static ImageLoopEffect()
        {
            EffectType = new EffectType(
                66, 0x04000020, 4, 1, 0, "画像ループ",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("横回数", 1, 1, 400, 1),
                    new TrackbarDefinition("縦回数", 1, 1, 400, 1),
                    new TrackbarDefinition("速度X", 10, -10000, 10000, 0),
                    new TrackbarDefinition("速度Y", 10, -10000, 10000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("個別オブジェクト", true, 0),
                }
            );
        }
    }
}
