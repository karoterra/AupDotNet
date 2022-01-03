namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 音声ディレイ
    /// </summary>
    public class AudioDelayEffect : Effect
    {
        /// <summary>
        /// 音声ディレイのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>強さ</summary>
        public Trackbar Gain => Trackbars[0];

        /// <summary>遅延(ms)</summary>
        public Trackbar Delay => Trackbars[1];

        /// <summary>
        /// <see cref="AudioDelayEffect"/> のインスタンスを初期化します。
        /// </summary>
        public AudioDelayEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="AudioDelayEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public AudioDelayEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static AudioDelayEffect()
        {
            EffectType = new EffectType(
                90, 0x04200020, 2, 0, 0, "音声ディレイ",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("強さ", 10, 0, 1000, 500),
                    new TrackbarDefinition("遅延(ms)", 1, 0, 1000, 100),
                },
                new CheckboxDefinition[] { }
            );
        }
    }
}
