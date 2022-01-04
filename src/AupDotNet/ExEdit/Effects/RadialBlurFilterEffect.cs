namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 放射ブラー(フィルタオブジェクト)
    /// </summary>
    public class RadialBlurFilterEffect : Effect
    {
        /// <summary>
        /// 放射ブラーのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>中心X</summary>
        public Trackbar X => Trackbars[1];

        /// <summary>中心Y</summary>
        public Trackbar Y => Trackbars[2];

        /// <summary>
        /// <see cref="RadialBlurFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        public RadialBlurFilterEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="RadialBlurFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public RadialBlurFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static RadialBlurFilterEffect()
        {
            EffectType = new EffectType(
                44, 0x44000000, 3, 0, 0, "放射ブラー",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("範囲", 10, 0, 1000, 200),
                    new TrackbarDefinition("X", 1, -2000, 2000, 0),
                    new TrackbarDefinition("Y", 1, -2000, 2000, 0),
                },
                new CheckboxDefinition[] { }
            );
        }
    }
}
