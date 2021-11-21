namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// フェード
    /// </summary>
    public class FadeEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.Fade;

        /// <summary>イン</summary>
        public Trackbar In => Trackbars[0];

        /// <summary>アウト</summary>
        public Trackbar Out => Trackbars[1];

        public FadeEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public FadeEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
