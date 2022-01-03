namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ノイズ除去フィルタ(AviUtl組込みフィルタ)
    /// </summary>
    public class DenoiseFilterEffect : Effect
    {
        /// <summary>
        /// ノイズ除去フィルタのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[1];

        /// <summary>しきい値</summary>
        public Trackbar Threshold => Trackbars[2];

        /// <summary>
        /// <see cref="DenoiseFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        public DenoiseFilterEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="DenoiseFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public DenoiseFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static DenoiseFilterEffect()
        {
            EffectType = new EffectType(
                100, 0x02000000, 3, 0, 0, "ノイズ除去フィルタ",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("強さ", 1, 0, 256, 256),
                    new TrackbarDefinition("範囲", 1, 1, 3, 2),
                    new TrackbarDefinition("しきい値", 1, 0, 256, 24),
                },
                new CheckboxDefinition[] { }
            );
        }
    }
}
