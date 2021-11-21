using System;
using System.Drawing;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// グラデーション
    /// </summary>
    public class GradationEffect : Effect
    {
        private const int Id = (int)EffectTypeId.Gradation;

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        /// <summary>中心X</summary>
        public Trackbar X => Trackbars[1];

        /// <summary>中心Y</summary>
        public Trackbar Y => Trackbars[2];

        /// <summary>角度</summary>
        public Trackbar Angle => Trackbars[3];

        /// <summary>幅</summary>
        public Trackbar Width => Trackbars[4];

        /// <summary>合成モード</summary>
        public BlendMode BlendMode { get; set; }

        /// <summary>開始色</summary>
        public Color Color { get; set; }

        /// <summary>色指定無し(開始色)</summary>
        public bool NoColor { get; set; }

        /// <summary>終了色</summary>
        public Color Color2 { get; set; }

        /// <summary>色指定無し(終了色)</summary>
        public bool NoColor2 { get; set; }

        /// <summary>
        /// グラデーションの形状
        /// <list type="bullet">
        ///     <item>0. 線</item>
        ///     <item>1. 円</item>
        ///     <item>2. 四角形</item>
        ///     <item>3. 凸形</item>
        /// </list>
        /// </summary>
        public int Shape { get; set; }

        public GradationEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public GradationEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public GradationEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    BlendMode = (BlendMode)span.Slice(0, 4).ToInt32();
                    Color = span.Slice(4, 4).ToColor();
                    NoColor = span[7] != 0;
                    Color2 = span.Slice(8, 4).ToColor();
                    NoColor2 = span[11] != 0;
                    Shape = span.Slice(12, 4).ToInt32();
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
            ((int)BlendMode).ToBytes().CopyTo(data, 0);
            Color.ToBytes().CopyTo(data, 4);
            data[7] = (byte)(NoColor ? 1 : 0);
            Color2.ToBytes().CopyTo(data, 8);
            data[11] = (byte)(NoColor2 ? 1 : 0);
            Shape.ToBytes().CopyTo(data, 12);
            return data;
        }
    }
}
