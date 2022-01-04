namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// スクリプトがあるディレクトリを表す。
    /// </summary>
    public enum ScriptDirectory
    {
        /// <summary>
        /// すべて表示
        /// </summary>
        All = 0,

        /// <summary>
        /// ルートフォルダ
        /// </summary>
        Root,

        /// <summary>
        /// script フォルダの1つ下のフォルダ。
        /// </summary>
        Other,
    }
}
