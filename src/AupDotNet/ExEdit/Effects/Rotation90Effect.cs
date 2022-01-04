namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ローテーション(基本効果)
    /// </summary>
    public class Rotation90Effect : Effect
    {
        /// <summary>
        /// ローテーションのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>90度回転</summary>
        public Trackbar Rotation => Trackbars[0];

        /// <summary>
        /// <see cref="Rotation90Effect"/> のインスタンスを初期化します。
        /// </summary>
        public Rotation90Effect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="Rotation90Effect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public Rotation90Effect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static Rotation90Effect()
        {
            EffectType = new EffectType(
                57, 0x04008020, 1, 0, 0, "ローテーション",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("90度回転", 1, -4, 4, 0),
                },
                new CheckboxDefinition[] { }
            );
        }
    }
}
