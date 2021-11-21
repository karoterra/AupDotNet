namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 音声フェード
    /// </summary>
    public class AudioFadeEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.AudioFade;

        /// <summary>イン</summary>
        public Trackbar In => Trackbars[0];

        /// <summary>アウト</summary>
        public Trackbar Out => Trackbars[1];

        public AudioFadeEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public AudioFadeEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
