namespace Karoterra.AupDotNet.ExEdit
{
    public abstract class Effect
    {
        public readonly EffectType Type;
        public EffectFlag Flag { get; set; }
        public virtual Trackbar[] Trackbars { get; }
        public virtual uint[] Checkboxes { get; }

        public Effect(EffectType effectType)
        {
            Type = effectType;
        }

        public abstract byte[] DumpExtData();
    }
}
