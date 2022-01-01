namespace Karoterra.AupDotNet.ExEdit
{
    /// <summary>
    /// <see cref="EffectType"/> が保持するチェックボックスの定義。
    /// </summary>
    public class CheckboxDefinition
    {
        /// <summary>
        /// チェックボックスの表示名。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// チェックボックスであるかどうか。
        /// チェックボックスなら <c>true</c>、ボタンやコンボボックスなら <c>false</c>。
        /// </summary>
        public bool IsCheckbox { get; set; }

        /// <summary>
        /// デフォルト値。
        /// </summary>
        public int Default { get; set; }

        /// <summary>
        /// <see cref="CheckboxDefinition"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="name">チェックボックスの表示名</param>
        /// <param name="isCheckbox">チェックボックスであるかどうか</param>
        /// <param name="def">デフォルト値</param>
        public CheckboxDefinition(string name, bool isCheckbox, int def)
        {
            Name = name;
            IsCheckbox = isCheckbox;
            Default = def;
        }
    }
}
