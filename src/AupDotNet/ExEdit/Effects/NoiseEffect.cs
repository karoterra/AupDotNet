using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ノイズ
    /// </summary>
    public class NoiseEffect : Effect
    {
        private const int Id = (int)EffectTypeId.Noise;

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

        public int Field0xC { get; set; }

        public NoiseEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public NoiseEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public NoiseEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    NoiseType = span.Slice(0, 4).ToInt32();
                    Mode = span.Slice(4, 4).ToInt32();
                    Seed = span.Slice(8, 4).ToInt32();
                    Field0xC = span.Slice(0xC, 4).ToInt32();
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
            NoiseType.ToBytes().CopyTo(data, 0);
            Mode.ToBytes().CopyTo(data, 4);
            Seed.ToBytes().CopyTo(data, 8);
            Field0xC.ToBytes().CopyTo(data, 0xC);
            return data;
        }
    }
}
