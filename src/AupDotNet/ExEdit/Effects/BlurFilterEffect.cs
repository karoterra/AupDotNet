namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ぼかし(フィルタオブジェクト)
    /// </summary>
    public class BlurFilterEffect : Effect
    {
        /// <summary>
        /// ぼかしのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>縦横比</summary>
        public Trackbar AspectRatio => Trackbars[1];

        /// <summary>光の強さ</summary>
        public Trackbar Intensity => Trackbars[2];

        /// <summary>
        /// <see cref="BlurFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        public BlurFilterEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="BlurFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public BlurFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static BlurFilterEffect()
        {
            EffectType = new EffectType(
                20, 0x04000000, 3, 0, 0, "ぼかし",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("範囲", 1, 0, 1000, 5),
                    new TrackbarDefinition("縦横比", 10, -1000, 1000, 0),
                    new TrackbarDefinition("光の強さ", 1, 0, 60, 0),
                },
                System.Array.Empty<CheckboxDefinition>()
            );
        }
    }
}
