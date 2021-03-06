namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 境界ぼかし
    /// </summary>
    public class BorderBlurEffect : Effect
    {
        /// <summary>
        /// 境界ぼかしのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>縦横比</summary>
        public Trackbar AspectRatio => Trackbars[1];

        /// <summary>透明度の境界をぼかす</summary>
        public bool BlurAlpha
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>
        /// <see cref="BorderBlurEffect"/> のインスタンスを初期化します。
        /// </summary>
        public BorderBlurEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="BorderBlurEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public BorderBlurEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static BorderBlurEffect()
        {
            EffectType = new EffectType(
                19, 0x04000020, 2, 1, 0, "境界ぼかし",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("範囲", 1, 0, 2000, 5),
                    new TrackbarDefinition("縦横比", 10, -1000, 1000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("透明度の境界をぼかす", true, 0),
                }
            );
        }
    }
}
