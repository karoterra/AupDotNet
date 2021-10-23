using System;
using System.Linq;

namespace Karoterra.AupDotNet.Extensions
{
    public static class BooleanExtensions
    {
        public static bool ToBool(this byte[] x)
        {
            return x.Any(y => y != 0);
        }

        public static bool ToBool(this ReadOnlySpan<byte> x)
        {
            foreach (var y in x)
            {
                if (y != 0) return true;
            }
            return false;
        }

        public static byte[] ToBytes(this bool x, int size = 4)
        {
            var b = new byte[size];
            if (x) b[0] = 1;
            return b;
        }
    }
}
