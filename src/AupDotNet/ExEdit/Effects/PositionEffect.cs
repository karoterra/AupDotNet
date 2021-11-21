namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 座標(基本効果)
    /// </summary>
    public class PositionEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.Position;

        public Trackbar X => Trackbars[0];
        public Trackbar Y => Trackbars[1];
        public Trackbar Z => Trackbars[2];

        public PositionEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public PositionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
