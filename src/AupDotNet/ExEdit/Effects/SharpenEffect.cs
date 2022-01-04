namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// シャープ
    /// </summary>
    public class SharpenEffect : Effect
    {
        /// <summary>
        /// シャープのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[1];

        /// <summary>
        /// <see cref="SharpenEffect"/> のインスタンスを初期化します。
        /// </summary>
        public SharpenEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="SharpenEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public SharpenEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static SharpenEffect()
        {
            EffectType = new EffectType(
                38, 0x04000020, 2, 0, 0, "シャープ",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("強さ", 10, 0, 8000, 500),
                    new TrackbarDefinition("範囲", 1, 0, 100, 5),
                },
                System.Array.Empty<CheckboxDefinition>()
            );
        }
    }
}
