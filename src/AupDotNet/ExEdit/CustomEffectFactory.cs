using Karoterra.AupDotNet.ExEdit.Effects;

namespace Karoterra.AupDotNet.ExEdit
{
    public class CustomEffectFactory : IEffectFactory
    {
        public Effect Create(EffectType type, Trackbar[] trackbars, int[] checkboxes, byte[] data)
        {
            return new CustomEffect(type, trackbars, checkboxes, data);
        }
    }
}
