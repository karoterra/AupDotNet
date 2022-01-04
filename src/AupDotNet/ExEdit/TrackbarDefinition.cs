namespace Karoterra.AupDotNet.ExEdit
{
    /// <summary>
    /// <see cref="EffectType"/> が保持するチェックボックスの定義。
    /// </summary>
    public class TrackbarDefinition
    {
        /// <summary>
        /// トラックバーの名前。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 値のスケール。
        /// </summary>
        /// <remarks>
        /// 値の移動単位が 1 なら <c>Scale</c>は 1、
        /// 0.1 なら <c>Scale</c> は 10、
        /// 0.01 なら <c>Scale</c> は 100。
        /// </remarks>
        public int Scale { get; set; }

        /// <summary>
        /// 値の最小値。
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// 値の最大値。
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// 値のデフォルト値。
        /// </summary>
        public int Default { get; set; }

        /// <summary>
        /// <see cref="TrackbarDefinition"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="name">名前</param>
        /// <param name="scale">スケール</param>
        /// <param name="min">最小値</param>
        /// <param name="max">最大値</param>
        /// <param name="def">デフォルト値</param>
        public TrackbarDefinition(string name, int scale, int min, int max, int def)
        {
            Name = name;
            Scale = scale;
            Min = min;
            Max = max;
            Default = def;
        }
    }
}
