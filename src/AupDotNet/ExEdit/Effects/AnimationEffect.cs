namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public class AnimationEffect : ScriptFileEffect
    {
        private const int Id = (int)EffectTypeId.AnimationEffect;

        public AnimationEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public AnimationEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public AnimationEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes, data)
        {
        }
    }
}
