namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// シャープ
    /// </summary>
    public class SharpenEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.Sharpen;

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[1];

        public SharpenEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public SharpenEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
