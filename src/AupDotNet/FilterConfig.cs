using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet
{
    /// <summary>
    /// AviUtl のプロファイルを表すクラス。
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// FilterConfigFile ヘッダー。
        /// </summary>
        public static readonly string Header = "AviUtl FilterConfigFile version 0.1\0";

        /// <summary>
        /// プロファイル名の最大バイト数。
        /// </summary>
        public static readonly int MaxNameLength = 260;

        private string _name = string.Empty;
        /// <summary>
        /// プロファイル名。
        /// </summary>
        /// <remarks>
        /// 長さの上限は Shift JIS エンコーディングで <see cref="MaxNameLength"/> バイトです。
        /// </remarks>
        public string Name
        {
            get => _name;
            set
            {
                if (value.GetSjisByteCount() >= MaxNameLength)
                {
                    throw new MaxByteCountOfStringException(nameof(Name), MaxNameLength);
                }
                _name = value;
            }
        }

        /// <summary>
        /// プロファイルのデータ。
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// 指定したプロファイル名とデータで <see cref="FilterConfig"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="name">プロファイル名</param>
        /// <param name="data">データ</param>
        public FilterConfig(string name, byte[] data)
        {
            Name = name;
            Data = data;
        }
    }
}
