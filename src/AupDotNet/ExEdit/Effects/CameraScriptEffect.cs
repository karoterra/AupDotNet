using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// スクリプト(カメラ制御)
    /// </summary>
    public class CameraScriptEffect : Effect
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

        public CameraScriptEffect()
            : base(EffectType)
        {
        }

        public CameraScriptEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }
        public CameraScriptEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
           : base(EffectType, trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    Text = span.Slice(0, MaxTextLength).ToCleanUTF16String();
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
            Text.ToUTF16Bytes(MaxTextLength).CopyTo(data, 0);
            return data;
        }

        static CameraScriptEffect()
        {
            EffectType = new EffectType(
                99, 0x05000400, 0, 0, 2048, "スクリプト(カメラ制御)",
                new TrackbarDefinition[] { },
                new CheckboxDefinition[] { }
            );
        }
    }
}
