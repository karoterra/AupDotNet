using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 時間制御
    /// </summary>
    public class TimeControlEffect : Effect
    {
        private const int Id = (int)EffectTypeId.TimeControl;

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
            : base(EffectType.Defaults[Id])
        {
        }

        public TimeControlEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public TimeControlEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    Range = span.Slice(0, 4).ToInt32();
                    span.Slice(4, 16).CopyTo(Data);
                }
                else if (data.Length != 0)
                {
                    throw new ArgumentException("data's length is invalid.");
                }
            }
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Range.ToBytes().CopyTo(data, 0);
            Data.CopyTo(data, 4);
            return data;
        }
    }
}
