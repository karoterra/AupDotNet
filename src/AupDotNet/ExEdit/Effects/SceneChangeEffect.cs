namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public class SceneChangeEffect : ScriptFileEffect
    {
        private const int Id = (int)EffectTypeId.SceneChange;

        public SceneChangeEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public SceneChangeEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public SceneChangeEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes, data)
        {
        }
    }
}
