using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// カラーキー
    /// </summary>
    public class ColorKeyEffect : Effect
    {
        /// <summary>
        /// カラーキーのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>輝度範囲</summary>
        public Trackbar LuminanceRange => Trackbars[0];

        /// <summary>色差範囲</summary>
        public Trackbar DifferenceRange => Trackbars[1];

        /// <summary>境界補正</summary>
        public Trackbar Boundary => Trackbars[2];

        /// <summary>キー色の取得</summary>
        public YCbCr Color { get; set; }

        /// <summary>キー(未取得)</summary>
        public int Status { get; set; }

        /// <summary>
        /// 拡張データのオフセットアドレス 0x6。
        /// </summary>
        public short Field0x6 { get; set; }


        /// <summary>
        /// <see cref="ColorKeyEffect"/> のインスタンスを初期化します。
        /// </summary>
        public ColorKeyEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="ColorKeyEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public ColorKeyEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="ColorKeyEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public ColorKeyEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Color = data.ToYCbCr();
            Field0x6 = data.Slice(6, 2).ToInt16();
            Status = data.Slice(8, 4).ToInt32();
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Color.ToBytes().CopyTo(data, 0);
            Field0x6.ToBytes().CopyTo(data, 6);
            Status.ToBytes().CopyTo(data, 8);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("color_yc=");
            writer.WriteLine(Color.ToString());
            writer.Write("status=");
            writer.WriteLine(Status);
        }

        static ColorKeyEffect()
        {
            EffectType = new EffectType(
                31, 0x04000420, 3, 1, 12, "カラーキー",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("輝度範囲", 1, 0, 4096, 0),
                    new TrackbarDefinition("色差範囲", 1, 0, 4096, 0),
                    new TrackbarDefinition("境界補正", 1, 0, 5, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("キー色の取得", false, 0),
                }
            );
        }
    }
}
