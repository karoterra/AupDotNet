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
        private const int Id = (int)EffectTypeId.CameraScript;

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
            : base(EffectType.Defaults[Id])
        {
        }

        public CameraScriptEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
        public CameraScriptEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
           : base(EffectType.Defaults[Id], trackbars, checkboxes)
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
    }
}
