namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 図形情報の種類
    /// </summary>
    public enum FigureNameType
    {
        /// <summary>
        /// 拡張編集の組込み図形。
        /// </summary>
        BuiltIn,

        /// <summary>
        /// figure フォルダの画像。
        /// </summary>
        Figure,

        /// <summary>
        /// 外部ファイル。
        /// </summary>
        File,

        /// <summary>
        /// シーン参照。
        /// </summary>
        Scene,
    }
}
