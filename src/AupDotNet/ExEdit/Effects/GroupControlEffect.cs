using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// グループ制御
    /// </summary>
    public class GroupControlEffect : Effect
    {
        /// <summary>
        /// グループ制御のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>X</summary>
        public Trackbar X => Trackbars[0];

        /// <summary>Y</summary>
        public Trackbar Y => Trackbars[1];

        /// <summary>Z</summary>
        public Trackbar Z => Trackbars[2];

        /// <summary>拡大率</summary>
        public Trackbar Zoom => Trackbars[3];

        /// <summary>X軸回転</summary>
        public Trackbar RotateX => Trackbars[4];

        /// <summary>Y軸回転</summary>
        public Trackbar RotateY => Trackbars[5];

        /// <summary>Z軸回転</summary>
        public Trackbar RotateZ => Trackbars[6];

        /// <summary>上位グループ制御の影響を受ける</summary>
        public bool Inherit
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>同じグループのオブジェクトを対象にする</summary>
        public bool OnlySameGroup
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>対象レイヤー数</summary>
        public int Range { get; set; }

        /// <summary>
        /// 拡張データの後半 16 バイト。
        /// </summary>
        public byte[] Data { get; } = new byte[16];

        /// <summary>
        /// <see cref="GroupControlEffect"/> のインスタンスを初期化します。
        /// </summary>
        public GroupControlEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="GroupControlEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public GroupControlEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="GroupControlEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public GroupControlEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Range = data.Slice(0, 4).ToInt32();
            data.Slice(4, 16).CopyTo(Data);
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Range.ToBytes().CopyTo(data, 0);
            Data.CopyTo(data, 4);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("range=");
            writer.WriteLine(Range);
        }

        static GroupControlEffect()
        {
            EffectType = new EffectType(
                94, 0x45000420, 7, 2, 20, "グループ制御",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("X", 10, -999999, 999999, 0),
                    new TrackbarDefinition("Y", 10, -999999, 999999, 0),
                    new TrackbarDefinition("Z", 10, -999999, 999999, 0),
                    new TrackbarDefinition("拡大率", 100, 0, 500000, 10000),
                    new TrackbarDefinition("X軸回転", 100, -360000, 360000, 0),
                    new TrackbarDefinition("Y軸回転", 100, -360000, 360000, 0),
                    new TrackbarDefinition("Z軸回転", 100, -360000, 360000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("上位グループ制御の影響を受ける", true, 0),
                    new CheckboxDefinition("同じグループのオブジェクトを対象にする", true, 1),
                }
            );
        }
    }
}
