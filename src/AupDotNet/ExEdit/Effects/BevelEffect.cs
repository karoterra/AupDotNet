namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 凸エッジ
    /// </summary>
    public class BevelEffect : Effect
    {
        /// <summary>
        /// 凸エッジのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>幅</summary>
        public Trackbar Width => Trackbars[0];

        /// <summary>高さ</summary>
        public Trackbar Height => Trackbars[1];

        /// <summary>角度</summary>
        public Trackbar Angle => Trackbars[2];

        /// <summary>
        /// <see cref="BevelEffect"/> のインスタンスを初期化します。
        /// </summary>
        public BevelEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="BevelEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public BevelEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static BevelEffect()
        {
            EffectType = new EffectType(
                36, 0x04000020, 3, 0, 0, "凸エッジ",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("幅", 1, 0, 100, 4),
                    new TrackbarDefinition("高さ", 100, 0, 300, 100),
                    new TrackbarDefinition("角度", 10, -3600, 3600, -450),
                },
                System.Array.Empty<CheckboxDefinition>()
            );
        }
    }
}
