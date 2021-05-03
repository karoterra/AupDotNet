using System;
using System.Drawing;

namespace Karoterra.AupDotNet.Extensions
{
    public static class ColorExtensions
    {
        public static Color ToColor(this ReadOnlySpan<byte> x)
        {
            return Color.FromArgb(x[0], x[1], x[2]);
        }

        public static byte[] ToBytes(this Color x)
        {
            var b = new byte[4] { x.R, x.G, x.B, 0 };
            return b;
        }
    }
}
