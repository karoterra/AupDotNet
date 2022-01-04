using System;
using System.Linq;

namespace Karoterra.AupDotNet.Extensions
{
    /// <summary>
    /// <see cref="Karoterra.AupDotNet"/> 内で使用する <c>bool</c> 用の拡張メソッドを提供します。
    /// </summary>
    public static class BooleanExtensions
    {
        /// <summary>
        /// 配列に非ゼロ値が含まれているかどうかを判定します。
        /// </summary>
        /// <param name="x">判定する配列</param>
        /// <returns>配列に非ゼロ値が含まれていれば <c>true</c>。それ以外の場合は <c>false</c>。</returns>
        public static bool ToBool(this byte[] x)
        {
            return x.Any(y => y != 0);
        }

        /// <summary>
        /// 配列に非ゼロ値が含まれているかどうかを判定します。
        /// </summary>
        /// <param name="x">判定する配列</param>
        /// <returns>配列に非ゼロ値が含まれていれば <c>true</c>。それ以外の場合は <c>false</c>。</returns>
        public static bool ToBool(this ReadOnlySpan<byte> x)
        {
            foreach (var y in x)
            {
                if (y != 0) return true;
            }
            return false;
        }

        /// <summary>
        /// 指定した <c>bool</c> 値を表す <c>byte</c> 配列を返します。
        /// </summary>
        /// <param name="x"><c>byte</c> 配列に変換する <c>bool</c>値</param>
        /// <param name="size">出力する <c>byte</c> 配列の長さ</param>
        /// <returns><c>bool</c> 値を表す <c>byte</c> 配列</returns>
        public static byte[] ToBytes(this bool x, int size = 4)
        {
            var b = new byte[size];
            if (x) b[0] = 1;
            return b;
        }
    }
}
