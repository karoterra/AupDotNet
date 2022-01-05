namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 回転(基本効果)
    /// </summary>
    public class RotationEffect : Effect
    {
        /// <summary>
        /// 回転のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>X</summary>
        public Trackbar X => Trackbars[0];

        /// <summary>Y</summary>
        public Trackbar Y => Trackbars[1];
        
        /// <summary>Z</summary>
        public Trackbar Z => Trackbars[2];

        /// <summary>
        /// <see cref="RotationEffect"/> のインスタンスを初期化します。
        /// </summary>
        public RotationEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="RotationEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public RotationEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static RotationEffect()
        {
            EffectType = new EffectType(
                54, 0x04008020, 3, 0, 0, "回転",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("X", 100, -360000, 360000, 0),
                    new TrackbarDefinition("Y", 100, -360000, 360000, 0),
                    new TrackbarDefinition("Z", 100, -360000, 360000, 0),
                },
                System.Array.Empty<CheckboxDefinition>()
            );
        }
    }
}
