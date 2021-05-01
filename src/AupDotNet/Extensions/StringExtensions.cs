using System;
using System.Text;

namespace Karoterra.AupDotNet.Extensions
{
    public static class StringExtensions
    {
        private static readonly Encoding sjis;

        static StringExtensions()
        {
            sjis = Encoding.GetEncoding(932);
        }

        public static string ToSjisString(this byte[] bytes)
        {
            return sjis.GetString(bytes);
        }

        public static string ToSjisString(this ReadOnlySpan<byte> bytes)
        {
            return sjis.GetString(bytes.ToArray());
        }

        public static byte[] ToSjisBytes(this string s)
        {
            return sjis.GetBytes(s);
        }

        public static byte[] ToSjisBytes(this string s, int length)
        {
            var bytes = new byte[length];
            sjis.GetBytes(s).CopyTo(bytes, 0);
            return bytes;
        }

        public static string CutNull(this string s)
        {
            return s.Split('\0')[0];
        }

        public static int GetSjisByteCount(this string s)
        {
            return sjis.GetByteCount(s);
        }
    }
}
