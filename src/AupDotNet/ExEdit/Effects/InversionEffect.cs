namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 反転(基本効果)
    /// </summary>
    public class InversionEffect : Effect
    {
        /// <summary>
        /// 反転のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>上下反転</summary>
        public bool Vertical
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>左右反転</summary>
        public bool Horizontal
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>輝度反転</summary>
        public bool Luminance
        {
            get => Checkboxes[2] != 0;
            set => Checkboxes[2] = value ? 1 : 0;
        }

        /// <summary>色相反転</summary>
        public bool Hue
        {
            get => Checkboxes[3] != 0;
            set => Checkboxes[3] = value ? 1 : 0;
        }

        /// <summary>透明度反転</summary>
        public bool Transparency
        {
            get => Checkboxes[4] != 0;
            set => Checkboxes[4] = value ? 1 : 0;
        }

        /// <summary>
        /// <see cref="InversionEffect"/> のインスタンスを初期化します。
        /// </summary>
        public InversionEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="InversionEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public InversionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static InversionEffect()
        {
            EffectType = new EffectType(
                60, 0x04008020, 0, 5, 0, "反転",
                new TrackbarDefinition[] { },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("上下反転", true, 0),
                    new CheckboxDefinition("左右反転", true, 0),
                    new CheckboxDefinition("輝度反転", true, 0),
                    new CheckboxDefinition("色相反転", true, 0),
                    new CheckboxDefinition("透明度反転", true, 0),
                }
            );
        }
    }
}
