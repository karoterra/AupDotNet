using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ノイズ
    /// </summary>
    public class NoiseEffect : Effect
    {
        /// <summary>
        /// ノイズのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        /// <summary>速度X</summary>
        public Trackbar SpeedX => Trackbars[1];

        /// <summary>速度Y</summary>
        public Trackbar SpeedY => Trackbars[2];

        /// <summary>変化速度</summary>
        public Trackbar NoiseSpeed => Trackbars[3];

        /// <summary>周期X</summary>
        public Trackbar PeriodX => Trackbars[4];

        /// <summary>周期Y</summary>
        public Trackbar PeriodY => Trackbars[5];

        /// <summary>しきい値</summary>
        public Trackbar Threshold => Trackbars[6];

        /// <summary>ノイズの種類</summary>
        public int NoiseType { get; set; }

        /// <summary>
        /// 合成モード
        /// 0: アルファ値と乗算
        /// 1: 輝度と乗算
        /// </summary>
        public int Mode { get; set; }

        /// <summary>シード</summary>
        public int Seed { get; set; }

        /// <summary>
        /// 拡張データのオフセットアドレス 0xC。
        /// </summary>
        public int Field0xC { get; set; }

        /// <summary>
        /// <see cref="NoiseEffect"/> のインスタンスを初期化します。
        /// </summary>
        public NoiseEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="NoiseEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public NoiseEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="NoiseEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public NoiseEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            NoiseType = data.Slice(0, 4).ToInt32();
            Mode = data.Slice(4, 4).ToInt32();
            Seed = data.Slice(8, 4).ToInt32();
            Field0xC = data.Slice(0xC, 4).ToInt32();
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            NoiseType.ToBytes().CopyTo(data, 0);
            Mode.ToBytes().CopyTo(data, 4);
            Seed.ToBytes().CopyTo(data, 8);
            Field0xC.ToBytes().CopyTo(data, 0xC);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("type=");
            writer.WriteLine(NoiseType);
            writer.Write("mode=");
            writer.WriteLine(Mode);
            writer.Write("seed=");
            writer.WriteLine(Seed);
        }

        static NoiseEffect()
        {
            EffectType = new EffectType(
                70, 0x04000420, 7, 3, 16, "ノイズ",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("強さ", 10, 0, 2000, 1000),
                    new TrackbarDefinition("速度X", 10, -4000, 8000, 0),
                    new TrackbarDefinition("速度Y", 10, -4000, 8000, 0),
                    new TrackbarDefinition("変化速度", 10, 0, 8000, 0),
                    new TrackbarDefinition("周期X", 100, 0, 10000, 100),
                    new TrackbarDefinition("周期Y", 100, 0, 10000, 100),
                    new TrackbarDefinition("しきい値", 10, 0, 1000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("アルファ値と乗算", false, 0),
                    new CheckboxDefinition("Type1", false, 0),
                    new CheckboxDefinition("設定", false, 0),
                }
            );
        }
    }
}
