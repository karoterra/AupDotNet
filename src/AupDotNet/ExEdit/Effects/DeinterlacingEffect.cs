using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// インターレース解除
    /// </summary>
    public class DeinterlacingEffect : Effect
    {
        /// <summary>
        /// インターレース解除のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>
        /// <list type="bullet">
        ///     <item>0. 奇数解除</item>
        ///     <item>1. 偶数解除</item>
        ///     <item>2. 二重化</item>
        /// </list>
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// <see cref="DeinterlacingEffect"/> のインスタンスを初期化します。
        /// </summary>
        public DeinterlacingEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="DeinterlacingEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public DeinterlacingEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="DeinterlacingEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public DeinterlacingEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Mode = data.Slice(0, 4).ToInt32();
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Mode.ToBytes().CopyTo(data, 0);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("type=");
            writer.WriteLine(Mode);
        }

        static DeinterlacingEffect()
        {
            EffectType = new EffectType(
                84, 0x04000420, 0, 1, 4, "インターレース解除",
                Array.Empty<TrackbarDefinition>(),
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("奇数解除", false, 0),
                }
            );
        }
    }
}
