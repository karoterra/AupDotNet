namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 音声ディレイ(フィルタオブジェクト)
    /// </summary>
    public class AudioDelayFilterEffect : Effect
    {
        public static EffectType EffectType { get; }

        /// <summary>強さ</summary>
        public Trackbar Gain => Trackbars[0];

        /// <summary>遅延(ms)</summary>
        public Trackbar Delay => Trackbars[1];

        public AudioDelayFilterEffect()
            : base(EffectType)
        {
        }

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
                new CheckboxDefinition[] { }
            );
        }
    }
}
