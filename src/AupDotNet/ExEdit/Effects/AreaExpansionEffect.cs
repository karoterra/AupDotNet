namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 領域拡張(基本効果)
    /// </summary>
    public class AreaExpansionEffect : Effect
    {
        /// <summary>
        /// 領域拡張のフィルタ効果定義
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

        /// <summary>塗りつぶし</summary>
        public bool Fill
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>
        /// <see cref="AreaExpansionEffect"/> のインスタンスを初期化します。
        /// </summary>
        public AreaExpansionEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="AreaExpansionEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public AreaExpansionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static AreaExpansionEffect()
        {
            EffectType = new EffectType(
                55, 0x04008020, 4, 1, 0, "領域拡張",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("上", 1, 0, 4000, 0),
                    new TrackbarDefinition("下", 1, 0, 4000, 0),
                    new TrackbarDefinition("左", 1, 0, 4000, 0),
                    new TrackbarDefinition("右", 1, 0, 4000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("塗りつぶし", true, 0),
                }
            );
        }
    }
}
