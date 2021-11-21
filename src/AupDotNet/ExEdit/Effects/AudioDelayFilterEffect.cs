namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 音声ディレイ(フィルタオブジェクト)
    /// </summary>
    public class AudioDelayFilterEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.AudioDelayFilter;

        /// <summary>強さ</summary>
        public Trackbar Gain => Trackbars[0];

        /// <summary>遅延(ms)</summary>
        public Trackbar Delay => Trackbars[1];

        public AudioDelayFilterEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public AudioDelayFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
