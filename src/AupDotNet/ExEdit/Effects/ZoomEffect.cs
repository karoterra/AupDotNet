namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 拡大率(基本効果)
    /// </summary>
    public class ZoomEffect : Effect
    {
        /// <summary>
        /// 拡大率のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>拡大率</summary>
        public Trackbar Zoom => Trackbars[0];

        /// <summary>X</summary>
        public Trackbar X => Trackbars[1];
        
        /// <summary>Y</summary>
        public Trackbar Y => Trackbars[2];

        /// <summary>
        /// <see cref="ZoomEffect"/> のインスタンスを初期化します。
        /// </summary>
        public ZoomEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="ZoomEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public ZoomEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static ZoomEffect()
        {
            EffectType = new EffectType(
                52, 0x04008020, 3, 0, 0, "拡大率",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("拡大率", 100, 0, 500000, 10000),
                    new TrackbarDefinition("X", 100, 0, 500000, 10000),
                    new TrackbarDefinition("Y", 100, 0, 500000, 10000),
                },
                System.Array.Empty<CheckboxDefinition>()
            );
        }
    }
}
