namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public class CameraEffect : ScriptFileEffect
    {
        private const int Id = (int)EffectTypeId.CameraEffect;

        public CameraEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public CameraEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public CameraEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes, data)
        {
        }
    }
}
