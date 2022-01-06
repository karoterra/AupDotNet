namespace Karoterra.AupDotNet
{
    /// <summary>
    /// クリップボードから貼り付けられた画像を表すクラス。
    /// </summary>
    public class ClippedImage
    {
        /// <summary>
        /// 画像データが無い時の <see cref="Handle"/> の値。
        /// </summary>
        public const uint NoDataHandle = 0;

        /// <summary>
        /// 画像のハンドル。
        /// </summary>
        public uint Handle { get; set; }

        /// <summary>
        /// 画像データ。
        /// </summary>
        /// <remarks>
        /// <c>BITMAPFILEHEADER</c> 無しの BMP ファイル。
        /// </remarks>
        public byte[] Data { get; set; }

        /// <summary>
        /// 指定したハンドルとデータで <see cref="ClippedImage"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="handle">ハンドル</param>
        /// <param name="data">画像データ</param>
        public ClippedImage(uint handle, byte[] data)
        {
            Handle = handle;
            Data = data;
        }
    }
}
