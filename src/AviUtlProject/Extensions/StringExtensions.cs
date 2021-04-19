using System;
using System.Linq;
using System.Text;

namespace Karoterra.AviUtlProject.Extensions
{
    static class StringExtensions
    {
        public static string ToSjisString(this byte[] bytes)
        {
            var sjis = Encoding.GetEncoding(932);
            return sjis.GetString(bytes.TakeWhile(c => c != 0).ToArray());
        }

        public static string ToSjisString(this ReadOnlySpan<byte> bytes)
        {
            var sjis = Encoding.GetEncoding(932);
            var index = bytes.IndexOf((byte)0);
            if (index < 0)
            {
                index = bytes.Length;
            }
            return sjis.GetString(bytes.Slice(0, index).ToArray());
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
    }
}
