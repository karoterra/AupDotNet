namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// モーションブラー
    /// </summary>
    public class MotionBlurEffect : Effect
    {
        /// <summary>
        /// モーションブラーのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>間隔</summary>
        public Trackbar Interval => Trackbars[0];

        /// <summary>分解能</summary>
        public Trackbar Resolution => Trackbars[1];

        /// <summary>残像</summary>
        public bool Afterimage
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>オフスクリーン描画</summary>
        public bool Offscreen
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>出力時に分解能を上げる</summary>
        public bool HighResOutput
        {
            get => Checkboxes[2] != 0;
            set => Checkboxes[2] = value ? 1 : 0;
        }

        /// <summary>
        /// <see cref="MotionBlurEffect"/> のインスタンスを初期化します。
        /// </summary>
        public MotionBlurEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="MotionBlurEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public MotionBlurEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static MotionBlurEffect()
        {
            EffectType = new EffectType(
                49, 0x04000020, 2, 3, 0, "モーションブラー",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("間隔", 1, 0, 100, 1),
                    new TrackbarDefinition("分解能", 1, 1, 25, 10),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("残像", true, 0),
                    new CheckboxDefinition("オフスクリーン描画", true, 1),
                    new CheckboxDefinition("出力時に分解能を上げる", true, 0),
                }
            );
        }
    }
}
