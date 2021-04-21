using System;
using System.Text;

namespace Karoterra.AviUtlProject.Extensions
{
    public static class StringExtensions
    {
        public static string ToSjisString(this byte[] bytes)
        {
            var sjis = Encoding.GetEncoding(932);
            return sjis.GetString(bytes);
        }

        public static string ToSjisString(this ReadOnlySpan<byte> bytes)
        {
            var sjis = Encoding.GetEncoding(932);
            return sjis.GetString(bytes.ToArray());
        }

        public static byte[] ToSjisBytes(this string s)
        {
            var sjis = Encoding.GetEncoding(932);
            return sjis.GetBytes(s);
        }

        public static byte[] ToSjisBytes(this string s, int length)
        {
            var sjis = Encoding.GetEncoding(932);
            var bytes = new byte[length];
            sjis.GetBytes(s).CopyTo(bytes, 0);
            return bytes;
        }

        public static string CutNull(this string s)
        {
            return s.Split('\0')[0];
        }
    }
}
