using System;
using System.Drawing;

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
    }
}
