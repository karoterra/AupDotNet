namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 斜めクリッピング
    /// </summary>
    public class DiagonalClippingEffect : Effect
    {
        /// <summary>
        /// 斜めクリッピングのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>中心X</summary>
        public Trackbar X => Trackbars[0];

        /// <summary>中心Y</summary>
        public Trackbar Y => Trackbars[1];

        /// <summary>角度</summary>
        public Trackbar Angle => Trackbars[2];

        /// <summary>ぼかし</summary>
        public Trackbar Blur => Trackbars[3];

        /// <summary>幅</summary>
        public Trackbar Width => Trackbars[4];

        /// <summary>
        /// <see cref="DiagonalClippingEffect"/> のインスタンスを初期化します。
        /// </summary>
        public DiagonalClippingEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="DiagonalClippingEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public DiagonalClippingEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static DiagonalClippingEffect()
        {
            EffectType = new EffectType(
                42, 0x04000020, 5, 0, 0, "斜めクリッピング",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("中心X", 1, -2000, 2000, 0),
                    new TrackbarDefinition("中心Y", 1, -2000, 2000, 0),
                    new TrackbarDefinition("角度", 10, -36000, 36000, 0),
                    new TrackbarDefinition("ぼかし", 1, 0, 2000, 1),
                    new TrackbarDefinition("幅", 1, -2000, 2000, 0),
                },
                System.Array.Empty<CheckboxDefinition>()
            );
        }
    }
}
