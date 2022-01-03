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
        /// <summary>
        /// テキストの最大バイト数。
        /// </summary>
        public readonly int MaxTextLength = 2048;

        /// <summary>
        /// スクリプト(カメラ制御)のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        private string _text = "";
        /// <summary>スクリプトテキスト</summary>
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
        /// <see cref="CameraScriptEffect"/> のインスタンスを初期化します。
        /// </summary>
        public CameraScriptEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="CameraScriptEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public CameraScriptEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="CameraScriptEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public CameraScriptEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
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
