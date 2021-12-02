namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 音声フェード
    /// </summary>
    public class AudioFadeEffect : NoExtDataEffect
    {
        public static EffectType EffectType { get; }

        /// <summary>イン</summary>
        public Trackbar In => Trackbars[0];

        /// <summary>アウト</summary>
        public Trackbar Out => Trackbars[1];

        public AudioFadeEffect()
            : base(EffectType)
        {
        }

        public AudioFadeEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static AudioFadeEffect()
        {
            EffectType = new EffectType(
                89, 0x04200020, 2, 0, 0, "音量フェード",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("イン", 100, 0, 1000, 50),
                    new TrackbarDefinition("アウト", 100, 0, 1000, 50),
                },
                new CheckboxDefinition[] { }
            );
        }
    }
}
