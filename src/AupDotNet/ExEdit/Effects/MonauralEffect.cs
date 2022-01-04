namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// モノラル化
    /// </summary>
    public class MonauralEffect : Effect
    {
        /// <summary>
        /// モノラル化のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>比率</summary>
        public Trackbar Ratio => Trackbars[0];

        /// <summary>
        /// <see cref="MonauralEffect"/> のインスタンスを初期化します。
        /// </summary>
        public MonauralEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="MonauralEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public MonauralEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static MonauralEffect()
        {
            EffectType = new EffectType(
                92, 0x04200020, 1, 0, 0, "モノラル化",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("比率", 1, -1000, 1000, 0),
                },
                new CheckboxDefinition[] { }
            );
        }
    }
}
