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

        public byte[] Data { get; } = new byte[16];

        public TimeControlEffect()
            : base(EffectType)
        {
        }

        public TimeControlEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public TimeControlEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Range = data.Slice(0, 4).ToInt32();
            data.Slice(4, 16).CopyTo(Data);
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Range.ToBytes().CopyTo(data, 0);
            Data.CopyTo(data, 4);
            return data;
        }

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
