using System;

namespace Karoterra.AupDotNet.Extensions
{
    public static class IntegerExtensions
    {
        public static short ToInt16(this byte[] x)
        {
            return (short)(x[0] + (x[1] << 8));
        }

        public static short ToInt16(this ReadOnlySpan<byte> x)
        {
            return (short)(x[0] + (x[1] << 8));
        }

        public static ushort ToUInt16(this byte[] x)
        {
            return (ushort)(x[0] + (x[1] << 8));
        }

        public static ushort ToUInt16(this ReadOnlySpan<byte> x)
        {
            return (ushort)(x[0] + (x[1] << 8));
        }

        public static int ToInt32(this byte[] x)
        {
            return x[0] + (x[1] << 8) + (x[2] << 16) + (x[3] << 24);
        }

        public static int ToInt32(this ReadOnlySpan<byte> x)
        {
            return x[0] + (x[1] << 8) + (x[2] << 16) + (x[3] << 24);
        }

        public static uint ToUInt32(this byte[] x)
        {
            return (uint)(x[0] + (x[1] << 8) + (x[2] << 16) + (x[3] << 24));
        }

        public static uint ToUInt32(this ReadOnlySpan<byte> x)
        {
            return (uint)(x[0] + (x[1] << 8) + (x[2] << 16) + (x[3] << 24));
        }

        public static byte[] ToBytes(this short x)
        {
            var b = new byte[2];
            b[0] = (byte)(x & 0xFF);
            b[1] = (byte)((x >> 8) & 0xFF);
            return b;
        }

        public static byte[] ToBytes(this ushort x)
        {
            var b = new byte[2];
            b[0] = (byte)(x & 0xFF);
            b[1] = (byte)((x >> 8) & 0xFF);
            return b;
        }

        public static byte[] ToBytes(this int x)
        {
            var b = new byte[4];
            b[0] = (byte)(x & 0xFF);
            b[1] = (byte)((x >> 8) & 0xFF);
            b[2] = (byte)((x >> 16) & 0xFF);
            b[3] = (byte)((x >> 24) & 0xFF);
            return b;
        }

        public static byte[] ToBytes(this uint x)
        {
            var b = new byte[4];
            b[0] = (byte)(x & 0xFF);
            b[1] = (byte)((x >> 8) & 0xFF);
            b[2] = (byte)((x >> 16) & 0xFF);
            b[3] = (byte)((x >> 24) & 0xFF);
            return b;
        }
    }
}
