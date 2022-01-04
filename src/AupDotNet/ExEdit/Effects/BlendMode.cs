namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 合成モード
    /// </summary>
    public enum BlendMode
    {
        /// <summary>通常</summary>
        Normal = 0,

        /// <summary>加算</summary>
        Add,

        /// <summary>減算</summary>
        Sub,

        /// <summary>乗算</summary>
        Multi,

        /// <summary>スクリーン</summary>
        Screen,

        /// <summary>オーバーレイ</summary>
        Overlay,

        /// <summary>比較(明)</summary>
        CompLight,

        /// <summary>比較(暗)</summary>
        CompDark,

        /// <summary>輝度</summary>
        Luminance,

        /// <summary>色差</summary>
        ColorDifference,

        /// <summary>陰影</summary>
        Shade,

        /// <summary>明暗</summary>
        LightDark,

        /// <summary>差分</summary>
        Difference,
    }
}
