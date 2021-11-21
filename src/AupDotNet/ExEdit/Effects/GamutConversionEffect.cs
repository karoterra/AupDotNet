using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 特定色域変換
    /// </summary>
    public class GamutConversionEffect : Effect
    {
        private const int Id = (int)EffectTypeId.GamutConversion;

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

        public GamutConversionEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public GamutConversionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public GamutConversionEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    Color = span.ToYCbCr();
                    Status = span.Slice(6, 2).ToBool();
                    Color2 = span.Slice(8, 6).ToYCbCr();
                    Status2 = span.Slice(14, 2).ToBool();
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
            Color.ToBytes().CopyTo(data, 0);
            Status.ToBytes(2).CopyTo(data, 6);
            Color2.ToBytes().CopyTo(data, 8);
            Status2.ToBytes(2).CopyTo(data, 14);
            return data;
        }
    }
}
