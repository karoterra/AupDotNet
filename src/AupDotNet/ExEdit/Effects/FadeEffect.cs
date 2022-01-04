namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// フェード
    /// </summary>
    public class FadeEffect : Effect
    {
        /// <summary>
        /// フェードのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>イン</summary>
        public Trackbar In => Trackbars[0];

        /// <summary>アウト</summary>
        public Trackbar Out => Trackbars[1];

        /// <summary>
        /// <see cref="FadeEffect"/> のインスタンスを初期化します。
        /// </summary>
        public FadeEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="FadeEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public FadeEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static FadeEffect()
        {
            EffectType = new EffectType(
                39, 0x04000020, 2, 0, 0, "フェード",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("イン", 100, 0, 1000, 50),
                    new TrackbarDefinition("アウト", 100, 0, 1000, 50),
                },
                new CheckboxDefinition[] { }
            );
        }
    }
}
