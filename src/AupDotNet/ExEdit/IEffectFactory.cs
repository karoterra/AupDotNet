namespace Karoterra.AupDotNet.ExEdit
{
    /// <summary>
    /// フィルタ効果のファクトリを表すインタフェース。
    /// </summary>
    public interface IEffectFactory
    {
        /// <summary>
        /// フィルタ効果を生成します。
        /// </summary>
        /// <param name="type">フィルタ効果定義</param>
        /// <param name="trackbars">トラックバー情報</param>
        /// <param name="checkboxes">チェックボックス情報</param>
        /// <param name="data">拡張データ</param>
        /// <returns>フィルタ効果</returns>
        Effect Create(EffectType type, Trackbar[] trackbars, int[] checkboxes, byte[] data);
    }
}
