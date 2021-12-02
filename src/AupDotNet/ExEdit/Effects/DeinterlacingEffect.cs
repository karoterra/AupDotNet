using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// インターレース解除
    /// </summary>
    public class DeinterlacingEffect : Effect
    {
        public static EffectType EffectType { get; }

        /// <summary>
        /// <list type="bullet">
        ///     <item>0. 奇数解除</item>
        ///     <item>1. 偶数解除</item>
        ///     <item>2. 二重化</item>
        /// </list>
        /// </summary>
        public int Mode { get; set; }

        public DeinterlacingEffect()
            : base(EffectType)
        {
        }

        public DeinterlacingEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public DeinterlacingEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType, trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    Mode = span.Slice(0, 4).ToInt32();
                }
                else if (data.Length != 0)
                {
                    throw new ArgumentException("data's length is invalid.");
                }
            }
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Mode.ToBytes().CopyTo(data, 0);
            return data;
        }

        static DeinterlacingEffect()
        {
            EffectType = new EffectType(
                84, 0x04000420, 0, 1, 4, "インターレース解除",
                new TrackbarDefinition[] { },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("奇数解除", false, 0),
                }
            );
        }
    }
}
