using System;

namespace Karoterra.AupDotNet.Extensions
{
    /// <summary>
    /// <see cref="Karoterra.AupDotNet"/> 内で使用する整数型用の拡張メソッドを提供します。
    /// </summary>
    public static class IntegerExtensions
    {
        /// <summary>
        /// <c>byte</c> 配列を リトルエンディアンとして 16 bit 符号付き整数に変換します。
        /// </summary>
        /// <param name="x">16 bit 符号付き整数のバイナリ表現</param>
        /// <returns>リトルエンディアンとして変換された 16 bit 符号付き整数</returns>
        public static short ToInt16(this byte[] x)
        {
            return (short)(x[0] + (x[1] << 8));
        }

        /// <summary>
        /// <c>byte</c> 配列を リトルエンディアンとして 16 bit 符号付き整数に変換します。
        /// </summary>
        /// <param name="x">16 bit 符号付き整数のバイナリ表現</param>
        /// <returns>リトルエンディアンとして変換された 16 bit 符号付き整数</returns>
        public static short ToInt16(this ReadOnlySpan<byte> x)
        {
            return (short)(x[0] + (x[1] << 8));
        }

        /// <summary>
        /// <c>byte</c> 配列を リトルエンディアンとして 16 bit 符号無し整数に変換します。
        /// </summary>
        /// <param name="x">16 bit 符号無し整数のバイナリ表現</param>
        /// <returns>リトルエンディアンとして変換された 16 bit 符号無し整数</returns>
        public static ushort ToUInt16(this byte[] x)
        {
            return (ushort)(x[0] + (x[1] << 8));
        }

        /// <summary>
        /// <c>byte</c> 配列を リトルエンディアンとして 16 bit 符号無し整数に変換します。
        /// </summary>
        /// <param name="x">16 bit 符号無し整数のバイナリ表現</param>
        /// <returns>リトルエンディアンとして変換された 16 bit 符号無し整数</returns>
        public static ushort ToUInt16(this ReadOnlySpan<byte> x)
        {
            return (ushort)(x[0] + (x[1] << 8));
        }

        /// <summary>
        /// <c>byte</c> 配列を リトルエンディアンとして 32 bit 符号付き整数に変換します。
        /// </summary>
        /// <param name="x">32 bit 符号付き整数のバイナリ表現</param>
        /// <returns>リトルエンディアンとして変換された 32 bit 符号付き整数</returns>
        public static int ToInt32(this byte[] x)
        {
            return x[0] + (x[1] << 8) + (x[2] << 16) + (x[3] << 24);
        }

        /// <summary>
        /// <c>byte</c> 配列を リトルエンディアンとして 32 bit 符号付き整数に変換します。
        /// </summary>
        /// <param name="x">32 bit 符号付き整数のバイナリ表現</param>
        /// <returns>リトルエンディアンとして変換された 32 bit 符号付き整数</returns>
        public static int ToInt32(this ReadOnlySpan<byte> x)
        {
            return x[0] + (x[1] << 8) + (x[2] << 16) + (x[3] << 24);
        }

        /// <summary>
        /// <c>byte</c> 配列を リトルエンディアンとして 32 bit 符号無し整数に変換します。
        /// </summary>
        /// <param name="x">32 bit 符号無し整数のバイナリ表現</param>
        /// <returns>リトルエンディアンとして変換された 32 bit 符号無し整数</returns>
        public static uint ToUInt32(this byte[] x)
        {
            return (uint)(x[0] + (x[1] << 8) + (x[2] << 16) + (x[3] << 24));
        }

        /// <summary>
        /// <c>byte</c> 配列を リトルエンディアンとして 32bit 符号無し整数に変換します。
        /// </summary>
        /// <param name="x">32 bit 符号無し整数のバイナリ表現</param>
        /// <returns>リトルエンディアンとして変換された 32 bit 符号無し整数</returns>
        public static uint ToUInt32(this ReadOnlySpan<byte> x)
        {
            return (uint)(x[0] + (x[1] << 8) + (x[2] << 16) + (x[3] << 24));
        }

        /// <summary>
        /// 16 bit 符号付き整数をリトルエンディアンとして <c>byte</c> 配列に変換します。
        /// </summary>
        /// <param name="x">変換する値</param>
        /// <returns><c>x</c> のリトルエンディアンでのバイナリ表現</returns>
        public static byte[] ToBytes(this short x)
        {
            var b = new byte[2];
            b[0] = (byte)(x & 0xFF);
            b[1] = (byte)((x >> 8) & 0xFF);
            return b;
        }

        /// <summary>
        /// 16 bit 符号無し整数をリトルエンディアンとして <c>byte</c> 配列に変換します。
        /// </summary>
        /// <param name="x">変換する値</param>
        /// <returns><c>x</c> のリトルエンディアンでのバイナリ表現</returns>
        public static byte[] ToBytes(this ushort x)
        {
            var b = new byte[2];
            b[0] = (byte)(x & 0xFF);
            b[1] = (byte)((x >> 8) & 0xFF);
            return b;
        }

        /// <summary>
        /// 32 bit 符号付き整数をリトルエンディアンとして <c>byte</c> 配列に変換します。
        /// </summary>
        /// <param name="x">変換する値</param>
        /// <returns><c>x</c> のリトルエンディアンでのバイナリ表現</returns>
        public static byte[] ToBytes(this int x)
        {
            var b = new byte[4];
            b[0] = (byte)(x & 0xFF);
            b[1] = (byte)((x >> 8) & 0xFF);
            b[2] = (byte)((x >> 16) & 0xFF);
            b[3] = (byte)((x >> 24) & 0xFF);
            return b;
        }

        /// <summary>
        /// 32 bit 符号無し整数をリトルエンディアンとして <c>byte</c> 配列に変換します。
        /// </summary>
        /// <param name="x">変換する値</param>
        /// <returns><c>x</c> のリトルエンディアンでのバイナリ表現</returns>
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
