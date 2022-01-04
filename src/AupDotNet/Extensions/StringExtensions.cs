using System;
using System.Text;

namespace Karoterra.AupDotNet.Extensions
{
    /// <summary>
    /// <see cref="Karoterra.AupDotNet"/> 内で使用する文字列用の拡張メソッドを提供します。
    /// </summary>
    public static class StringExtensions
    {
        private static readonly Encoding sjis;

        static StringExtensions()
        {
            sjis = Encoding.GetEncoding(932);
        }

        /// <summary>
        /// バイナリ全体を Shift_JIS として文字列に変換します。
        /// </summary>
        /// <remarks>
        /// このメソッドはヌル文字以降も文字列に変換します。
        /// </remarks>
        /// <param name="bytes">文字列に変換するバイナリ</param>
        /// <returns>Shift_JIS としてデコードされた文字列</returns>
        public static string ToSjisString(this byte[] bytes)
        {
            return sjis.GetString(bytes);
        }

        /// <summary>
        /// バイナリ全体を Shift_JIS として文字列に変換します。
        /// </summary>
        /// <remarks>
        /// このメソッドはヌル文字以降も文字列に変換します。
        /// </remarks>
        /// <param name="bytes">文字列に変換するバイナリ</param>
        /// <returns>Shift_JIS としてデコードされた文字列</returns>
        public static string ToSjisString(this ReadOnlySpan<byte> bytes)
        {
#if NET6_0_OR_GREATER
            return sjis.GetString(bytes);
#else
            return sjis.GetString(bytes.ToArray());
#endif
        }

        /// <summary>
        /// バイナリを Shift_JIS としてヌル文字までを文字列に変換します。
        /// </summary>
        /// <remarks>
        /// このメソッドはヌル文字以降は文字列に変換しません。
        /// </remarks>
        /// <param name="bytes">文字列に変換するバイナリ</param>
        /// <returns>Shift_JIS としてデコードされた文字列</returns>
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

        /// <summary>
        /// バイナリを Shift_JIS としてヌル文字までを文字列に変換します。
        /// </summary>
        /// <remarks>
        /// このメソッドはヌル文字以降は文字列に変換しません。
        /// </remarks>
        /// <param name="bytes">文字列に変換するバイナリ</param>
        /// <returns>Shift_JIS としてデコードされた文字列</returns>
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
#if NET6_0_OR_GREATER
            return sjis.GetString(bytes[..count]);
#else
            return sjis.GetString(bytes.Slice(0, count).ToArray());
#endif
        }

        /// <summary>
        /// バイナリ全体を UTF-16LE として文字列に変換します。
        /// </summary>
        /// <remarks>
        /// このメソッドはヌル文字以降も文字列に変換します。
        /// </remarks>
        /// <param name="bytes">文字列に変換するバイナリ</param>
        /// <returns>UTF-16LE としてデコードされた文字列</returns>
        public static string ToUTF16String(this ReadOnlySpan<byte> bytes)
        {
#if NET6_0_OR_GREATER
            return Encoding.Unicode.GetString(bytes);
#else
            return Encoding.Unicode.GetString(bytes.ToArray());
#endif
        }

        /// <summary>
        /// バイナリを UTF-16LE としてヌル文字までを文字列に変換します。
        /// </summary>
        /// <remarks>
        /// このメソッドはヌル文字以降は文字列に変換しません。
        /// </remarks>
        /// <param name="bytes">文字列に変換するバイナリ</param>
        /// <returns>UTF-16LE としてデコードされた文字列</returns>
        public static string ToCleanUTF16String(this ReadOnlySpan<byte> bytes)
        {
            return bytes.ToUTF16String().CutNull();
        }

        /// <summary>
        /// 文字列を Shift_JIS としてエンコードします。
        /// </summary>
        /// <param name="s">エンコードする文字列</param>
        /// <returns>Shift_JIS としてエンコードされたバイナリ</returns>
        public static byte[] ToSjisBytes(this string s)
        {
            return sjis.GetBytes(s);
        }

        /// <summary>
        /// 文字列を Shift_JIS としてバイト配列の長さを指定してエンコードします。
        /// バイト配列の後半は0埋めされます。
        /// </summary>
        /// <param name="s">エンコードする文字列</param>
        /// <param name="length">バイト配列の長さ</param>
        /// <returns>Shift_JIS としてエンコードされたバイナリ</returns>
        public static byte[] ToSjisBytes(this string s, int length)
        {
            var bytes = new byte[length];
            sjis.GetBytes(s).CopyTo(bytes, 0);
            return bytes;
        }

        /// <summary>
        /// 文字列を UTF-16LE としてエンコードします。
        /// </summary>
        /// <param name="s">エンコードする文字列</param>
        /// <returns>UTF-16LE としてエンコードされたバイナリ</returns>
        public static byte[] ToUTF16Bytes(this string s)
        {
            return Encoding.Unicode.GetBytes(s);
        }

        /// <summary>
        /// 文字列を UTF-16LE としてバイト配列の長さを指定してエンコードします。
        /// バイト配列の後半は0埋めされます。
        /// </summary>
        /// <param name="s">エンコードする文字列</param>
        /// <param name="length">バイト配列の長さ</param>
        /// <returns>UTF-16LE としてエンコードされたバイナリ</returns>
        public static byte[] ToUTF16Bytes(this string s, int length)
        {
            var bytes = new byte[length];
            Encoding.Unicode.GetBytes(s).CopyTo(bytes, 0);
            return bytes;
        }

        /// <summary>
        /// 文字列を最初のヌル文字まででカットします。
        /// </summary>
        /// <param name="s">カットする文字列</param>
        /// <returns>カットされた文字列</returns>
        public static string CutNull(this string s)
        {
            return s.Split('\0')[0];
        }

        /// <summary>
        /// 文字列を Shift_JIS としてエンコードしたときに何バイトになるか計算します。
        /// </summary>
        /// <param name="s">バイト数を調べる文字列</param>
        /// <returns>文字列を Shift_JIS としてエンコードしたときのバイト数</returns>
        public static int GetSjisByteCount(this string s)
        {
            return sjis.GetByteCount(s);
        }

        /// <summary>
        /// 文字列を UTF-16LE としてエンコードしたときに何バイトになるか計算します。
        /// </summary>
        /// <param name="s">バイト数を調べる文字列</param>
        /// <returns>文字列を UTF-16LE としてエンコードしたときのバイト数</returns>
        public static int GetUTF16ByteCount(this string s)
        {
            return Encoding.Unicode.GetByteCount(s);
        }

        /// <summary>
        /// 文字列を指定したバイト数で UTF-16LE の16進文字列にエンコードします。
        /// </summary>
        /// <param name="s">エンコードする文字列</param>
        /// <param name="length">バイト数</param>
        /// <returns>UTF-16LE としてエンコードした16進文字列</returns>
        public static string ToUTF16ByteString(this string s, int length)
        {
            byte[] bytes = s.ToUTF16Bytes(length);
            StringBuilder builder = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
                builder.Append(b.ToString("x2"));
            return builder.ToString();
        }
    }
}
