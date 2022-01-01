using System;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// フィルタ効果定義が不明なフィルタ効果を表す <see cref="Effect"/>。
    /// </summary>
    public class CustomEffect : Effect
    {
        /// <summary>
        /// 拡張データ
        /// </summary>
        public byte[] Data { get; }

        public CustomEffect(EffectType type)
            : base(type)
        {
            Data = new byte[Type.ExtSize];
        }

        public CustomEffect(EffectType type, Trackbar[] trackbars, int[] checkboxes)
            : base(type, trackbars, checkboxes)
        {
            Data = new byte[Type.ExtSize];
        }


        public CustomEffect(EffectType type, Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(type, trackbars, checkboxes)
        {
            Data = new byte[Type.ExtSize];
            ParseExtData(data);
        }

        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            data.CopyTo(Data);
        }

        public override byte[] DumpExtData()
        {
            return Data;
        }
    }
}
