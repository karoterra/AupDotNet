namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 放射ブラー
    /// </summary>
    public class RadialBlurEffect : Effect
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

        /// <summary>サイズ固定</summary>
        public bool FixSize
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>
        /// <see cref="RadialBlurEffect"/> のインスタンスを初期化します。
        /// </summary>
        public RadialBlurEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="RadialBlurEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public RadialBlurEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static RadialBlurEffect()
        {
            EffectType = new EffectType(
                43, 0x04000020, 3, 1, 0, "放射ブラー",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("範囲", 10, 0, 750, 200),
                    new TrackbarDefinition("X", 1, -2000, 2000, 0),
                    new TrackbarDefinition("Y", 1, -2000, 2000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("サイズ固定", true, 0),
                }
            );
        }
    }
}
