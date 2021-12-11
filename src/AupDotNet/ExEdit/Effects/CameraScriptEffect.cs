using System;
using System.IO;
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
        public CameraScriptEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
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

        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("text=");
            writer.WriteLine(Text.ToUTF16ByteString(MaxTextLength));
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
