namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// オフスクリーン描画
    /// </summary>
    public class OffScreenEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.OffScreen;

        public OffScreenEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public OffScreenEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
