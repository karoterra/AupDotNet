using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 波紋
    /// </summary>
    public class RippleEffect : Effect
    {
        public static EffectType EffectType { get; }

        /// <summary>中心X</summary>
        public Trackbar X => Trackbars[0];

        /// <summary>中心Y</summary>
        public Trackbar Y => Trackbars[1];

        /// <summary>幅</summary>
        public Trackbar Wavelength => Trackbars[2];

        /// <summary>高さ</summary>
        public Trackbar Amplitude => Trackbars[3];

        /// <summary>速度</summary>
        public Trackbar Speed => Trackbars[4];

        /// <summary>波紋数</summary>
        public int Num { get; set; }

        /// <summary>波紋間隔</summary>
        public int Interval { get; set; }

        /// <summary>増幅減衰回数</summary>
        public int Add { get; set; }

        public RippleEffect()
            : base(EffectType)
        {
        }

        public RippleEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public RippleEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType, trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    Num = span.Slice(0, 4).ToInt32();
                    Interval = span.Slice(4, 4).ToInt32();
                    Add = span.Slice(8, 4).ToInt32();
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
            Num.ToBytes().CopyTo(data, 0);
            Interval.ToBytes().CopyTo(data, 4);
            Add.ToBytes().CopyTo(data, 8);
            return data;
        }

        static RippleEffect()
        {
            EffectType = new EffectType(
                65, 0x04000420, 5, 1, 12, "波紋",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("中心X", 1, -4000, 4000, 0),
                    new TrackbarDefinition("中心Y", 1, -4000, 4000, 0),
                    new TrackbarDefinition("幅", 10, 10, 4000, 300),
                    new TrackbarDefinition("高さ", 10, -4000, 4000, 150),
                    new TrackbarDefinition("速度", 10, -20000, 20000, 1500),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("詳細設定", false, 0),
                }
            );
        }
    }
}
