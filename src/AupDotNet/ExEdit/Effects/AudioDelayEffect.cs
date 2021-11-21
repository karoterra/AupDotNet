namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 音声ディレイ
    /// </summary>
    public class AudioDelayEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.AudioDelay;

        /// <summary>強さ</summary>
        public Trackbar Gain => Trackbars[0];

        /// <summary>遅延(ms)</summary>
        public Trackbar Delay => Trackbars[1];

        public AudioDelayEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public AudioDelayEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
