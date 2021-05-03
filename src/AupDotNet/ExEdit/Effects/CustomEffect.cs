using System;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public class CustomEffect : Effect
    {
        public byte[] Data { get; }

        public CustomEffect(EffectType type)
            : base(type)
        {
            Data = new byte[Type.ExtSize];
        }

        public CustomEffect(EffectType type, Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(type, trackbars, checkboxes)
        {
            if (data.Length == 0)
            {
                Data = new byte[Type.ExtSize];
            }
            else if (data.Length == Type.ExtSize)
            {
                Data = data;
            }
            else
            {
                throw new ArgumentException("data's length is invalid.");
            }
        }

        public override byte[] DumpExtData()
        {
            return Data;
        }
    }
}
