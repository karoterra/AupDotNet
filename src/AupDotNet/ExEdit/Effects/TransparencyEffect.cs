namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 透明度(基本効果)
    /// </summary>
    public class TransparencyEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.Transparency;

        /// <summary>透明度</summary>
        public Trackbar Transparency => Trackbars[0];

        public TransparencyEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public TransparencyEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
