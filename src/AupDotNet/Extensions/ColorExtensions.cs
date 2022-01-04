using System;
using System.Drawing;
using Karoterra.AupDotNet.ExEdit;

namespace Karoterra.AupDotNet.Extensions
{
    /// <summary>
    /// <see cref="Karoterra.AupDotNet"/> 内で使用する色を表すクラス用の拡張メソッドを提供します。
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// <c>byte</c> 配列を RRGGBBAA として <see cref="Color"/> に変換します。
        /// </summary>
        /// <param name="x"><see cref="Color"/> に変換する <c>byte</c> 配列</param>
        /// <param name="alpha"><c>true</c> の場合は4バイト目をアルファ値として読み取ります。</param>
        /// <returns>変換された <see cref="Color"/></returns>
        public static Color ToColor(this ReadOnlySpan<byte> x, bool alpha = false)
        {
            if (alpha) return Color.FromArgb(x[3], x[0], x[1], x[2]);
            else return Color.FromArgb(x[0], x[1], x[2]);
        }

        /// <summary>
        /// <see cref="Color"/> を RRGGBBAA として <c>byte</c> 配列に変換します。
        /// </summary>
        /// <param name="x"><c>byte</c> 配列に変換する <see cref="Color"/></param>
        /// <param name="alpha"><c>true</c> の場合は4バイト目にアルファ値が入ります。<c>false</c> の場合は4バイト目は0です。</param>
        /// <returns><see cref="Color"/> を表す <c>byte</c> 配列</returns>
        public static byte[] ToBytes(this Color x, bool alpha = false)
        {
            var b = new byte[4]
            {
                x.R, x.G, x.B,
                alpha ? x.A : (byte)0
            };
            return b;
        }

        /// <summary>
        /// <c>byte</c> 配列を <see cref="YCbCr"/> に変換します。
        /// </summary>
        /// <param name="x"><see cref="YCbCr"/> に変換する <c>byte</c> 配列</param>
        /// <returns>変換された <see cref="YCbCr"/></returns>
        public static YCbCr ToYCbCr(this ReadOnlySpan<byte> x)
        {
            short y = x.ToInt16();
            short cb = x.Slice(2).ToInt16();
            short cr = x.Slice(4).ToInt16();
            return new YCbCr(y, cb, cr);
        }
    }
}
