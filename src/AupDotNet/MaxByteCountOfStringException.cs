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
        /// <summary>
        /// <see cref="MaxByteCountOfStringException"/> のインスタンスを初期化します。
        /// </summary>
        public MaxByteCountOfStringException()
            : base()
        {
        }

        /// <summary>
        /// 指定したエラーメッセージを使用して、<see cref="MaxByteCountOfStringException"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="message">例外の原因を説明するエラーメッセージ。</param>
        public MaxByteCountOfStringException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 指定したエラーメッセージおよびこの例外の原因となった内部例外への参照を使用して、<see cref="MaxByteCountOfStringException"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="message">例外の原因を説明するエラーメッセージ。</param>
        /// <param name="innerException">現在の例外の原因となった例外。</param>
        public MaxByteCountOfStringException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 指定した識別子への操作中に指定した最大バイト長を超えたことを表す <see cref="MaxByteCountOfStringException"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="name">識別子名</param>
        /// <param name="maxSize">文字列の最大バイト長</param>
        public MaxByteCountOfStringException(string name, int maxSize)
            : base($"Byte count of {name} must be less than {maxSize}.")
        {
        }
    }
}
