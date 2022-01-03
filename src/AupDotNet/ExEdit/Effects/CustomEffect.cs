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

        /// <summary>
        /// フィルタ効果定義を指定して <see cref="CustomEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="type">フィルタ効果定義</param>
        public CustomEffect(EffectType type)
            : base(type)
        {
            Data = new byte[Type.ExtSize];
        }

        /// <summary>
        /// フィルタ効果定義とトラックバー、チェックボックスの値を指定して <see cref="CustomEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="type">フィルタ効果定義</param>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public CustomEffect(EffectType type, Trackbar[] trackbars, int[] checkboxes)
            : base(type, trackbars, checkboxes)
        {
            Data = new byte[Type.ExtSize];
        }

        /// <summary>
        /// フィルタ効果定義とトラックバー、チェックボックス、拡張データを指定して <see cref="CustomEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="type">フィルタ効果定義</param>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public CustomEffect(EffectType type, Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(type, trackbars, checkboxes)
        {
            Data = new byte[Type.ExtSize];
            ParseExtData(data);
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            data.CopyTo(Data);
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            return Data;
        }
    }
}
