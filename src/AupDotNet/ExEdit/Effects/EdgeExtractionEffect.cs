using System;
using System.Drawing;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// エッジ抽出
    /// </summary>
    public class EdgeExtractionEffect : Effect
    {
        /// <summary>
        /// エッジ抽出のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        /// <summary>しきい値</summary>
        public Trackbar Threshold => Trackbars[1];

        /// <summary>輝度エッジを抽出</summary>
        public bool LuminanceEdge
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>透明度エッジを抽出</summary>
        public bool TransparencyEdge
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>色の設定</summary>
        public Color Color { get; set; }

        /// <summary>
        /// <see cref="EdgeExtractionEffect"/> のインスタンスを初期化します。
        /// </summary>
        public EdgeExtractionEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="EdgeExtractionEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public EdgeExtractionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="EdgeExtractionEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public EdgeExtractionEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Color = data.Slice(0, 4).ToColor(true);
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Color.ToBytes(true).CopyTo(data, 0);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("color=");
            writer.WriteLine(ExeditUtil.ColorToString(Color));
        }

        static EdgeExtractionEffect()
        {
            EffectType = new EffectType(
                37, 0x04000420, 2, 3, 4, "エッジ抽出",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("強さ", 10, 0, 10000, 1000),
                    new TrackbarDefinition("しきい値", 100, -10000, 10000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("輝度エッジを抽出", true, 1),
                    new CheckboxDefinition("透明度エッジを抽出", true, 0),
                    new CheckboxDefinition("色の設定", false, 0),
                }
            );
        }
    }
}
