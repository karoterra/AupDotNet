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

        public static string ToCleanSjisString(this byte[] bytes)
        {
            int count = 0;
            bool full = false;
            foreach (byte b in bytes)
            {
                if (b == 0) break;
                if (full)
                {
                    count += 2;
                    full = false;
                }
                else
                {
                    if ((b <= 0x7F) || (0xA0 <= b && b <= 0xDF)) count++;
                    else full = true;
                }
            }
            return sjis.GetString(bytes, 0, count);
        }

        public static string ToCleanSjisString(this ReadOnlySpan<byte> bytes)
        {
            int count = 0;
            bool full = false;
            foreach (byte b in bytes)
            {
                if (b == 0) break;
                if (full)
                {
                    count += 2;
                    full = false;
                }
                else
                {
                    if ((b <= 0x7F) || (0xA0 <= b && b <= 0xDF)) count++;
                    else full = true;
                }
            }
            return sjis.GetString(bytes.Slice(0, count).ToArray());
        }

        public static string ToUTF16String(this ReadOnlySpan<byte> bytes)
        {
            return Encoding.Unicode.GetString(bytes.ToArray());
        }

        public static string ToCleanUTF16String(this ReadOnlySpan<byte> bytes)
        {
            return bytes.ToUTF16String().CutNull();
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
