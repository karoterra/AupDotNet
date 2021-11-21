namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 回転(基本効果)
    /// </summary>
    public class RotationEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.Rotation;

        public Trackbar X => Trackbars[0];
        public Trackbar Y => Trackbars[1];
        public Trackbar Z => Trackbars[2];

        public RotationEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public RotationEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
