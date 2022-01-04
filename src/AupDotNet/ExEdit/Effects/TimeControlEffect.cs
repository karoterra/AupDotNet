using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 時間制御
    /// </summary>
    public class TimeControlEffect : Effect
    {
        /// <summary>
        /// 時間制御のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>位置</summary>
        public Trackbar Position => Trackbars[0];

        /// <summary>繰り返し</summary>
        public Trackbar Repeat => Trackbars[1];

        /// <summary>コマ落ち</summary>
        public Trackbar DropFrame => Trackbars[2];

        /// <summary>フレーム番号指定</summary>
        public bool FrameMode
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>対象レイヤー数</summary>
        public int Range { get; set; }

        /// <summary>
        /// 拡張データの後半 16 バイト。
        /// </summary>
        public byte[] Data { get; } = new byte[16];

        /// <summary>
        /// <see cref="TimeControlEffect"/> のインスタンスを初期化します。
        /// </summary>
        public TimeControlEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="TimeControlEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public TimeControlEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="TimeControlEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public TimeControlEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
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

        static TimeControlEffect()
        {
            EffectType = new EffectType(
                93, 0x05000400, 3, 1, 20, "時間制御",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("位置", 100, 0, 10000, 0),
                    new TrackbarDefinition("繰り返し", 1, 1, 100, 1),
                    new TrackbarDefinition("コマ落ち", 1, 1, 100, 1),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("フレーム番号指定", true, 0),
                }
            );
        }
    }
}
