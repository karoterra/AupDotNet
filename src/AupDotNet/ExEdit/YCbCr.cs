using System;
using System.Text;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit
{
    /// <summary>
    /// YCbCr 色空間で表現される色を表します。
    /// </summary>
    public struct YCbCr : IEquatable<YCbCr>
    {
        /// <summary>
        /// 輝度
        /// </summary>
        public short Y { get; }

        /// <summary>
        /// 色差(青)
        /// </summary>
        public short Cb { get; }

        /// <summary>
        /// 色差(赤)
        /// </summary>
        public short Cr { get; }

        /// <summary>
        /// 指定した Y, Cb, Cr の値から <see cref="YCbCr"/> 構造体を作成します。
        /// </summary>
        /// <param name="y">輝度 Y</param>
        /// <param name="cb">色差(青) Cb</param>
        /// <param name="cr">色差(赤) Cr</param>
        public YCbCr(int y, int cb, int cr)
        {
            Y = (short)y;
            Cb = (short)cb;
            Cr = (short)cr;
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
            => obj is YCbCr other && Equals(other);

        /// <inheritdoc/>
        public bool Equals(YCbCr other)
            => Y == other.Y && Cb == other.Cb && Cr == other.Cr;

        /// <inheritdoc/>
        public override int GetHashCode()
            => Y ^ Cb ^ Cr;

        /// <inheritdoc/>
        public static bool operator ==(YCbCr lhs, YCbCr rhs) => lhs.Equals(rhs);

        /// <inheritdoc/>
        public static bool operator !=(YCbCr lhs, YCbCr rhs) => !(lhs == rhs);

        /// <summary>
        /// <see cref="YCbCr"/> をバイト配列に変換します。
        /// </summary>
        /// <returns>長さ 6 のバイト配列</returns>
        public byte[] ToBytes()
        {
            var b = new byte[6];
            Y.ToBytes().CopyTo(b, 0);
            Cb.ToBytes().CopyTo(b, 2);
            Cr.ToBytes().CopyTo(b, 4);
            return b;
        }

        /// <summary>
        /// <see cref="YCbCr"/> の値を表す16進文字列を返します。
        /// </summary>
        /// <returns>16進文字列</returns>
        public override string ToString()
        {
            var builder = new StringBuilder(4 * 3);
            builder.Append($"{(byte)(Y & 0xff):x2}");
            builder.Append($"{(byte)(Y >> 8):x2}");
            builder.Append($"{(byte)(Cb & 0xff):x2}");
            builder.Append($"{(byte)(Cb >> 8):x2}");
            builder.Append($"{(byte)(Cr & 0xff):x2}");
            builder.Append($"{(byte)(Cr >> 8):x2}");
            return builder.ToString();
        }
    }
}
