using System;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public abstract class NoExtDataEffect : Effect
    {
        public  NoExtDataEffect(EffectType type)
            : base(type)
        {
            if (Type.ExtSize != 0)
            {
                throw new ArgumentException("ExtSize must be zero.");
            }
        }

        public NoExtDataEffect(EffectType type, Trackbar[] trackbars, int[] checkboxes)
            : base(type, trackbars, checkboxes)
        {
            if (Type.ExtSize != 0)
            {
                throw new ArgumentException("ExtSize must be zero.");
            }
        }

        public override byte[] DumpExtData()
        {
            return new byte[0];
        }
    }
}
