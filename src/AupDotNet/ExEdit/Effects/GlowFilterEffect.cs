using System;
using System.Drawing;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// グロー(フィルタオブジェクト)
    /// </summary>
    public class GlowFilterEffect : Effect
    {
        /// <summary>
        /// グローのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        /// <summary>拡散</summary>
        public Trackbar Diffusion => Trackbars[1];

        /// <summary>しきい値</summary>
        public Trackbar Threshold => Trackbars[2];

        /// <summary>ぼかし</summary>
        public Trackbar Blur => Trackbars[3];

        /// <summary>光色の設定</summary>
        public Color Color { get; set; }

        /// <summary>光色の設定(色指定なし)</summary>
        public bool NoColor { get; set; }

        /// <summary>
        /// 形状
        /// </summary>
        /// <remarks>
        /// <list type="table">
        ///     <listheader>
        ///         <term>値</term>
        ///         <description>発光する部分の形状</description>
        ///     </listheader>
        ///     <item><term>0</term><description>通常</description></item>
        ///     <item><term>1</term><description>クロス(4本)</description></item>
        ///     <item><term>2</term><description>クロス(4本斜め)</description></item>
        ///     <item><term>3</term><description>クロス(8本)</description></item>
        ///     <item><term>4</term><description>ライン(横)</description></item>
        ///     <item><term>5</term><description>ライン(縦)</description></item>
        /// </list>
        /// </remarks>
        public int ShapeType { get; set; }

        /// <summary>
        /// <see cref="GlowFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        public GlowFilterEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="GlowFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public GlowFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="GlowFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public GlowFilterEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Color = data.ToColor();
            NoColor = data[3] != 0;
            ShapeType = data.Slice(4, 4).ToInt32();
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Color.ToBytes().CopyTo(data, 0);
            data[3] = (byte)(NoColor ? 1 : 0);
            ShapeType.ToBytes().CopyTo(data, 4);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("color=");
            writer.WriteLine(ExeditUtil.ColorToString(Color));
            writer.Write("no_color=");
            writer.WriteLine(NoColor ? '1' : '0');
            writer.Write("type=");
            writer.WriteLine(ShapeType);
        }

        static GlowFilterEffect()
        {
            EffectType = new EffectType(
                29, 0x04000400, 4, 2, 8, "グロー",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("強さ", 10, 0, 4000, 400),
                    new TrackbarDefinition("拡散", 1, 0, 200, 30),
                    new TrackbarDefinition("しきい値", 10, 0, 2000, 400),
                    new TrackbarDefinition("ぼかし", 1, 0, 50, 1),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("通常", false, 0),
                    new CheckboxDefinition("光色の設定", false, 0),
                }
            );
        }
    }
}
