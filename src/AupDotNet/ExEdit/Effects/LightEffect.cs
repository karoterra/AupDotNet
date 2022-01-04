using System;
using System.Drawing;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ライト
    /// </summary>
    public class LightEffect : Effect
    {
        /// <summary>
        /// ライトのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        /// <summary>拡散</summary>
        public Trackbar Diffusion => Trackbars[1];

        /// <summary>比率</summary>
        public Trackbar Rate => Trackbars[2];

        /// <summary>逆光</summary>
        public bool Backlight
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>色の設定</summary>
        public Color Color { get; set; }

        /// <summary>
        /// <see cref="LightEffect"/> のインスタンスを初期化します。
        /// </summary>
        public LightEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="LightEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public LightEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="LightEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public LightEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Color = data.ToColor(true);
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

        static LightEffect()
        {
            EffectType = new EffectType(
                33, 0x04000420, 3, 2, 4, "ライト",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("強さ", 10, 0, 3000, 1000),
                    new TrackbarDefinition("拡散", 1, 0, 500, 25),
                    new TrackbarDefinition("比率", 10, -1000, 1000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("逆光", true, 0),
                    new CheckboxDefinition("色の設定", false, 0),
                }
            );
        }
    }
}
