namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 音声ディレイ(フィルタオブジェクト)
    /// </summary>
    public class AudioDelayFilterEffect : Effect
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
        /// <see cref="AudioDelayFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        public AudioDelayFilterEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="AudioDelayFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public AudioDelayFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static AudioDelayFilterEffect()
        {
            EffectType = new EffectType(
                91, 0x04200000, 2, 0, 0, "音声ディレイ",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("強さ", 10, 0, 1000, 500),
                    new TrackbarDefinition("遅延(ms)", 1, 0, 1000, 100),
                },
                System.Array.Empty<CheckboxDefinition>()
            );
        }
    }
}
