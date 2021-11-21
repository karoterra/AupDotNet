using System;
using System.Drawing;
using Karoterra.AupDotNet.ExEdit;

namespace Karoterra.AupDotNet.Extensions
{
    public static class ColorExtensions
    {
        public static Color ToColor(this ReadOnlySpan<byte> x, bool alpha = false)
        {
            if (alpha) return Color.FromArgb(x[3], x[0], x[1], x[2]);
            else return Color.FromArgb(x[0], x[1], x[2]);
        }

        public static byte[] ToBytes(this Color x, bool alpha = false)
        {
            var b = new byte[4]
            {
                x.R, x.G, x.B,
                alpha ? x.A : (byte)0
            };
            return b;
        }

        public static YCbCr ToYCbCr(this ReadOnlySpan<byte> x)
        {
            short y = x.ToInt16();
            short cb = x.Slice(2).ToInt16();
            short cr = x.Slice(4).ToInt16();
            return new YCbCr(y, cb, cr);
        }
    }
}
