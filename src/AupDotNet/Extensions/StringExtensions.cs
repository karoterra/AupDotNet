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

        public static string ToUTF16String(this ReadOnlySpan<byte> bytes)
        {
            return Encoding.Unicode.GetString(bytes.ToArray());
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

        public static byte[] ToUTF16Bytes(this string s)
        {
            return Encoding.Unicode.GetBytes(s);
        }

        public static byte[] ToUTF16Bytes(this string s, int length)
        {
            var bytes = new byte[length];
            Encoding.Unicode.GetBytes(s).CopyTo(bytes, 0);
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

        public static int GetUTF16ByteCount(this string s)
        {
            return Encoding.Unicode.GetByteCount(s);
        }
    }
}
