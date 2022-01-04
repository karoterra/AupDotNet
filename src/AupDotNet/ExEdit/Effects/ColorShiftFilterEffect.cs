using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 色ずれ(フィルタオブジェクト)
    /// </summary>
    public class ColorShiftFilterEffect : Effect
    {
        /// <summary>
        /// 色ずれのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>ずれ幅</summary>
        public Trackbar Shift => Trackbars[0];

        /// <summary>角度</summary>
        public Trackbar Angle => Trackbars[1];

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[2];

        /// <summary>
        /// 色ずれの種類
        /// <list type="bullet">
        ///     <item>0. 赤緑</item>
        ///     <item>1. 赤青</item>
        ///     <item>2. 緑青</item>
        /// </list>
        /// </summary>
        public int ShiftType { get; set; }

        /// <summary>
        /// <see cref="ColorShiftFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        public ColorShiftFilterEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="ColorShiftFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public ColorShiftFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="ColorShiftFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public ColorShiftFilterEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            ShiftType = data.Slice(0, 4).ToInt32();
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            ShiftType.ToBytes().CopyTo(data, 0);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("type=");
            writer.WriteLine(ShiftType);
        }

        static ColorShiftFilterEffect()
        {
            EffectType = new EffectType(
                72, 0x04000400, 3, 1, 4, "色ずれ",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("ずれ幅", 1, 0, 2000, 5),
                    new TrackbarDefinition("角度", 10, -36000, 36000, 0),
                    new TrackbarDefinition("強さ", 1, 0, 100, 100),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("赤緑", false, 0),
                }
            );
        }
    }
}
