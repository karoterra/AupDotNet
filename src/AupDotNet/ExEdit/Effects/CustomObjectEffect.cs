namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public class CustomObjectEffect : ScriptFileEffect
    {
        private const int Id = (int)EffectTypeId.CustomObject;

        public CustomObjectEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public CustomObjectEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public CustomObjectEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes, data)
        {
        }
    }
}
