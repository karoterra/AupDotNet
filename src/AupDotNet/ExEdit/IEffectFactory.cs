namespace Karoterra.AupDotNet.ExEdit
{
    public interface IEffectFactory
    {
        Effect Create(EffectType type, Trackbar[] trackbars, int[] checkboxes, byte[] data);
    }
}
