using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Karoterra.AupDotNet.ExEdit
{
    public static class ExeditUtil
    {
        public static string ColorToString(Color color)
        {
            return $"{color.R:x2}{color.G:x2}{color.B:x2}";
        }

        public static string BytesToString(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
                builder.Append(b.ToString("x2"));
            return builder.ToString();
        }
    }
}
