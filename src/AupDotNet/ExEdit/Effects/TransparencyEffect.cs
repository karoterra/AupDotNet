namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 透明度(基本効果)
    /// </summary>
    public class TransparencyEffect : Effect
    {
        /// <summary>
        /// 透明度のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>透明度</summary>
        public Trackbar Transparency => Trackbars[0];

        /// <summary>
        /// <see cref="TransparencyEffect"/> のインスタンスを初期化します。
        /// </summary>
        public TransparencyEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="TransparencyEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public TransparencyEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static TransparencyEffect()
        {
            EffectType = new EffectType(
                53, 0x04008020, 1, 0, 0, "透明度",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("透明度", 10, 0, 1000, 0),
                },
                System.Array.Empty<CheckboxDefinition>()
            );
        }
    }
}
