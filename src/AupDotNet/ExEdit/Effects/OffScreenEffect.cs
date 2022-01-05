namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// オフスクリーン描画
    /// </summary>
    public class OffScreenEffect : Effect
    {
        /// <summary>
        /// オフスクリーン描画のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>
        /// <see cref="OffScreenEffect"/> のインスタンスを初期化します。
        /// </summary>
        public OffScreenEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="OffScreenEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public OffScreenEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static OffScreenEffect()
        {
            EffectType = new EffectType(
                86, 0x04000020, 0, 0, 0, "オフスクリーン描画",
                System.Array.Empty<TrackbarDefinition>(),
                System.Array.Empty<CheckboxDefinition>()
            );
        }
    }
}
