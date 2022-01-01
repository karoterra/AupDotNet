using System;
using System.Runtime.Serialization;

namespace Karoterra.AupDotNet
{
    /// <summary>
    /// 文字列のバイト長が上限を超えたときにスローされる例外。
    /// </summary>
    [Serializable()]
    public class MaxByteCountOfStringException : Exception
    {
        public MaxByteCountOfStringException()
            : base()
        {
        }

        public MaxByteCountOfStringException(string message)
            : base(message)
        {
        }

        public MaxByteCountOfStringException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public MaxByteCountOfStringException(string name, int maxSize)
            : base($"Byte count of {name} must be less than {maxSize}.")
        {
        }

        protected MaxByteCountOfStringException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
