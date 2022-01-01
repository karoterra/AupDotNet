using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Karoterra.AupDotNet.ExEdit
{
    /// <summary>
    /// <see cref="Karoterra.AupDotNet.ExEdit"/> でつかうユーティリティ。
    /// </summary>
    public static class ExeditUtil
    {
        /// <summary>
        /// <see cref="Color"/> を rrggbb 形式の文字列に変換します。
        /// </summary>
        /// <param name="color">文字列にする色</param>
        /// <returns>rrggbb 形式の文字列</returns>
        public static string ColorToString(Color color)
        {
            return $"{color.R:x2}{color.G:x2}{color.B:x2}";
        }

        /// <summary>
        /// <c>byte</c> 配列を16進数の文字列に変換します。
        /// </summary>
        /// <param name="bytes">データ</param>
        /// <returns>16進数の文字列</returns>
        public static string BytesToString(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
                builder.Append(b.ToString("x2"));
            return builder.ToString();
        }
    }
}
