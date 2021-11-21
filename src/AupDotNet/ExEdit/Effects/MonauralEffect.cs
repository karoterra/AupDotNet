namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// モノラル化
    /// </summary>
    public class MonauralEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.Monaural;

        /// <summary>比率</summary>
        public Trackbar Ratio => Trackbars[0];

        public MonauralEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public MonauralEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
