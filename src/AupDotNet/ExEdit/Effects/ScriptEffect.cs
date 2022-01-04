using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// スクリプト制御
    /// </summary>
    public class ScriptEffect : Effect
    {
        /// <summary>
        /// テキストの最大バイト数。
        /// </summary>
        public readonly int MaxTextLength = 2048;

        /// <summary>
        /// スクリプト制御のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        private string _text = "";
        /// <summary>
        /// エディットボックスに入力されたスクリプト。
        /// </summary>
        /// <remarks>
        /// 最大バイト数は <see cref="MaxTextLength"/> です。
        /// </remarks>
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

        /// <summary>
        /// <see cref="ScriptEffect"/> のインスタンスを初期化します。
        /// </summary>
        public ScriptEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="ScriptEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public ScriptEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="ScriptEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public ScriptEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Text = data.Slice(0, MaxTextLength).ToCleanUTF16String();
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Text.ToUTF16Bytes(MaxTextLength).CopyTo(data, 0);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("text=");
            writer.WriteLine(Text.ToUTF16ByteString(MaxTextLength));
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
