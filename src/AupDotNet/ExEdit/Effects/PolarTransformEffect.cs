namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 極座標変換
    /// </summary>
    public class PolarTransformEffect : Effect
    {
        /// <summary>
        /// 極座標変換のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>中心幅</summary>
        public Trackbar Margin => Trackbars[0];

        /// <summary>拡大率</summary>
        public Trackbar Zoom => Trackbars[1];

        /// <summary>回転</summary>
        public Trackbar Rotation => Trackbars[2];

        /// <summary>渦巻</summary>
        public Trackbar Spiral => Trackbars[3];

        /// <summary>
        /// <see cref="PolarTransformEffect"/> のインスタンスを初期化します。
        /// </summary>
        public PolarTransformEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="PolarTransformEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public PolarTransformEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static PolarTransformEffect()
        {
            EffectType = new EffectType(
                68, 0x04000020, 4, 0, 0, "極座標変換",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("中心幅", 1, 0, 2000, 0),
                    new TrackbarDefinition("拡大率", 10, 1000, 8000, 1000),
                    new TrackbarDefinition("回転", 10, -36000, 36000, 0),
                    new TrackbarDefinition("渦巻", 100, -800, 800, 0),
                },
                new CheckboxDefinition[] { }
            );
        }
    }
}
