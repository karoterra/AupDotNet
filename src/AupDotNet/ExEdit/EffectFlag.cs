using System;

namespace Karoterra.AupDotNet.ExEdit
{
    /// <summary>
    /// フィルタ効果のフラグ
    /// </summary>
    [Flags]
    public enum EffectFlag
    {
        /// <summary>
        /// フィルタ効果が有効
        /// </summary>
        Enable = 1,

        /// <summary>
        /// 設定ダイアログで畳んで表示
        /// </summary>
        Fold = 2,

        /// <summary>
        /// メインウィンドウ上でマウスを使って操作可能(設定ダイアログのマウスのアイコン)
        /// </summary>
        Interactive = 4,
    }
}
