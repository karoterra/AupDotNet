using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 特定色域変換
    /// </summary>
    public class GamutConversionEffect : Effect
    {
        /// <summary>
        /// 特定色域変換のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>色相範囲</summary>
        public Trackbar Hue => Trackbars[0];

        /// <summary>彩度範囲</summary>
        public Trackbar Chroma => Trackbars[1];

        /// <summary>境界補正</summary>
        public Trackbar Border => Trackbars[2];

        /// <summary>変換前の色</summary>
        public YCbCr Color { get; set; }

        /// <summary>変換前の色(未取得)</summary>
        public bool Status { get; set; }

        /// <summary>変換後の色</summary>
        public YCbCr Color2 { get; set; }

        /// <summary>変換後の色(未取得)</summary>
        public bool Status2 { get; set; }

        /// <summary>
        /// <see cref="GamutConversionEffect"/> のインスタンスを初期化します。
        /// </summary>
        public GamutConversionEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="GamutConversionEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public GamutConversionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="GamutConversionEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public GamutConversionEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Color = data.ToYCbCr();
            Status = data.Slice(6, 2).ToBool();
            Color2 = data.Slice(8, 6).ToYCbCr();
            Status2 = data.Slice(14, 2).ToBool();
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Color.ToBytes().CopyTo(data, 0);
            Status.ToBytes(2).CopyTo(data, 6);
            Color2.ToBytes().CopyTo(data, 8);
            Status2.ToBytes(2).CopyTo(data, 14);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("color_yc=");
            writer.WriteLine(Color.ToString());
            writer.Write("status=");
            writer.WriteLine(Status ? '1' : '0');
            writer.Write("color_yc2=");
            writer.WriteLine(Color2.ToString());
            writer.Write("status2=");
            writer.WriteLine(Status2 ? '1' : '0');
        }

        static GamutConversionEffect()
        {
            EffectType = new EffectType(
                78, 0x04000420, 3, 2, 16, "特定色域変換",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("色相範囲", 1, 0, 256, 8),
                    new TrackbarDefinition("彩度範囲", 1, 0, 256, 8),
                    new TrackbarDefinition("境界補正", 1, 0, 8, 2),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("変換前の色の取得", false, 0),
                    new CheckboxDefinition("変換後の色の取得", false, 0),
                }
            );
        }
    }
}
