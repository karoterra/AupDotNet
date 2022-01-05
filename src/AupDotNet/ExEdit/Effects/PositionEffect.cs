namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 座標(基本効果)
    /// </summary>
    public class PositionEffect : Effect
    {
        /// <summary>
        /// 座標のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>X</summary>
        public Trackbar X => Trackbars[0];

        /// <summary>Y</summary>
        public Trackbar Y => Trackbars[1];

        /// <summary>Z</summary>
        public Trackbar Z => Trackbars[2];

        /// <summary>
        /// <see cref="PositionEffect"/> のインスタンスを初期化します。
        /// </summary>
        public PositionEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="PositionEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public PositionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static PositionEffect()
        {
            EffectType = new EffectType(
                51, 0x04008020, 3, 0, 0, "座標",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("X", 10, -999999, 999999, 0),
                    new TrackbarDefinition("Y", 10, -999999, 999999, 0),
                    new TrackbarDefinition("Z", 10, -999999, 999999, 0),
                },
                System.Array.Empty<CheckboxDefinition>()
            );
        }
    }
}
