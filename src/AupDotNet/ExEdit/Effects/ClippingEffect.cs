namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// クリッピング
    /// </summary>
    public class ClippingEffect : Effect
    {
        /// <summary>
        /// クリッピングのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>上</summary>
        public Trackbar Top => Trackbars[0];

        /// <summary>下</summary>
        public Trackbar Bottom => Trackbars[1];
        
        /// <summary>左</summary>
        public Trackbar Left => Trackbars[2];
        
        /// <summary>右</summary>
        public Trackbar Right => Trackbars[3];

        /// <summary>中心の位置を変更</summary>
        public bool Centering
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>
        /// <see cref="ClippingEffect"/> のインスタンスを初期化します。
        /// </summary>
        public ClippingEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="ClippingEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public ClippingEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static ClippingEffect()
        {
            EffectType = new EffectType(
                17, 0x04000020, 4, 1, 0, "クリッピング",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("上", 1, 0, 4000, 0),
                    new TrackbarDefinition("下", 1, 0, 4000, 0),
                    new TrackbarDefinition("左", 1, 0, 4000, 0),
                    new TrackbarDefinition("右", 1, 0, 4000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("中心の位置を変更", true, 0),
                }
            );
        }
    }
}
