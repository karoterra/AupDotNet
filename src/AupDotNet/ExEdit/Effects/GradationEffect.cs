using System;
using System.Drawing;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// グラデーション
    /// </summary>
    public class GradationEffect : Effect
    {
        /// <summary>
        /// グラデーションのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        /// <summary>中心X</summary>
        public Trackbar X => Trackbars[1];

        /// <summary>中心Y</summary>
        public Trackbar Y => Trackbars[2];

        /// <summary>角度</summary>
        public Trackbar Angle => Trackbars[3];

        /// <summary>幅</summary>
        public Trackbar Width => Trackbars[4];

        /// <summary>合成モード</summary>
        public BlendMode BlendMode { get; set; }

        /// <summary>開始色</summary>
        public Color Color { get; set; }

        /// <summary>色指定無し(開始色)</summary>
        public bool NoColor { get; set; }

        /// <summary>終了色</summary>
        public Color Color2 { get; set; }

        /// <summary>色指定無し(終了色)</summary>
        public bool NoColor2 { get; set; }

        /// <summary>
        /// グラデーションの形状
        /// </summary>
        /// <remarks>
        /// <list type="table">
        ///     <listheader>
        ///         <term>値</term>
        ///         <description>グラデーションの形状</description>
        ///     </listheader>
        ///     <item><term>0</term><description>線</description></item>
        ///     <item><term>1</term><description>円</description></item>
        ///     <item><term>2</term><description>四角形</description></item>
        ///     <item><term>3</term><description>凸形</description></item>
        /// </list>
        /// </remarks>
        public int Shape { get; set; }

        /// <summary>
        /// <see cref="GradationEffect"/> のインスタンスを初期化します。
        /// </summary>
        public GradationEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="GradationEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public GradationEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="GradationEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public GradationEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            BlendMode = (BlendMode)data.Slice(0, 4).ToInt32();
            Color = data.Slice(4, 4).ToColor();
            NoColor = data[7] != 0;
            Color2 = data.Slice(8, 4).ToColor();
            NoColor2 = data[11] != 0;
            Shape = data.Slice(12, 4).ToInt32();
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            ((int)BlendMode).ToBytes().CopyTo(data, 0);
            Color.ToBytes().CopyTo(data, 4);
            data[7] = (byte)(NoColor ? 1 : 0);
            Color2.ToBytes().CopyTo(data, 8);
            data[11] = (byte)(NoColor2 ? 1 : 0);
            Shape.ToBytes().CopyTo(data, 12);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("blend=");
            writer.WriteLine((int)BlendMode);
            writer.Write("color=");
            writer.WriteLine(ExeditUtil.ColorToString(Color));
            writer.Write("no_color=");
            writer.WriteLine(NoColor ? '1' : '0');
            writer.Write("color2=");
            writer.WriteLine(ExeditUtil.ColorToString(Color2));
            writer.Write("no_color2=");
            writer.WriteLine(NoColor2 ? '1' : '0');
            writer.Write("type=");
            writer.WriteLine(Shape);
        }

        static GradationEffect()
        {
            EffectType = new EffectType(
                75, 0x04000420, 5, 4, 16, "グラデーション",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("強さ", 10, 0, 1000, 1000),
                    new TrackbarDefinition("中心X", 1, -2000, 2000, 0),
                    new TrackbarDefinition("中心Y", 1, -2000, 2000, 0),
                    new TrackbarDefinition("角度", 10, -36000, 36000, 0),
                    new TrackbarDefinition("幅", 1, 0, 2000, 100),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("通常", false, 0),
                    new CheckboxDefinition("線", false, 0),
                    new CheckboxDefinition("開始色", false, 0),
                    new CheckboxDefinition("終了色", false, 0),
                }
            );
        }
    }
}
