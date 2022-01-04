namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 拡散光(フィルタオブジェクト)
    /// </summary>
    public class DiffuseFilterEffect : Effect
    {
        /// <summary>
        /// 拡散光のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        /// <summary>拡散</summary>
        public Trackbar Diffusion => Trackbars[1];

        /// <summary>
        /// <see cref="DiffuseFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        public DiffuseFilterEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="DiffuseFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public DiffuseFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static DiffuseFilterEffect()
        {
            EffectType = new EffectType(
                27, 0x04000000, 2, 0, 0, "拡散光",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("強さ", 10, 0, 1000, 500),
                    new TrackbarDefinition("拡散", 1, 0, 500, 12),
                },
                System.Array.Empty<CheckboxDefinition>()
            );
        }
    }
}
