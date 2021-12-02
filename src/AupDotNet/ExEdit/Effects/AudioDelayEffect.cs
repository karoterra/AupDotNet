namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 音声ディレイ
    /// </summary>
    public class AudioDelayEffect : Effect
    {
        public static EffectType EffectType { get; }

        /// <summary>強さ</summary>
        public Trackbar Gain => Trackbars[0];

        /// <summary>遅延(ms)</summary>
        public Trackbar Delay => Trackbars[1];

        public AudioDelayEffect()
            : base(EffectType)
        {
        }

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
