using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// スクリプト制御
    /// </summary>
    public class ScriptEffect : Effect
    {
        public readonly int MaxTextLength = 2048;
        public static EffectType EffectType { get; }

        private string _text = "";
        public string Text
        {
            get => _text;
            set
            {
                if (value.GetUTF16ByteCount() >= MaxTextLength)
                {
                    throw new MaxByteCountOfStringException(nameof(Text), MaxTextLength);
                }
                _text = value;
            }
        }

        public ScriptEffect()
            : base(EffectType)
        {
        }

        public ScriptEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }
        public ScriptEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
           : base(EffectType, trackbars, checkboxes, data)
        {
        }

        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Text = data.Slice(0, MaxTextLength).ToCleanUTF16String();
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Text.ToUTF16Bytes(MaxTextLength).CopyTo(data, 0);
            return data;
        }

        static ScriptEffect()
        {
            EffectType = new EffectType(
                81, 0x04000420, 0, 0, 2048, "スクリプト制御",
                new TrackbarDefinition[] { },
                new CheckboxDefinition[] { }
            );
        }
    }
}
