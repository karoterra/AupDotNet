namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// オブジェクト分割
    /// </summary>
    public class SplitEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.Split;

        /// <summary>横分割数</summary>
        public Trackbar Horizontal => Trackbars[0];

        /// <summary>縦分割数</summary>
        public Trackbar Vertical => Trackbars[1];

        public SplitEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public SplitEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
