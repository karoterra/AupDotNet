namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// シャドー(カメラ制御)
    /// </summary>
    public class CameraShadowEffect : Effect
    {
        /// <summary>
        /// シャドー(カメラ制御)のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>光源X</summary>
        public Trackbar X => Trackbars[0];

        /// <summary>光源Y</summary>
        public Trackbar Y => Trackbars[1];

        /// <summary>光源Z</summary>
        public Trackbar Z => Trackbars[2];

        /// <summary>濃さ</summary>
        public Trackbar Depth => Trackbars[3];

        /// <summary>精度</summary>
        public Trackbar Precision => Trackbars[4];

        /// <summary>
        /// <see cref="CameraShadowEffect"/> のインスタンスを初期化します。
        /// </summary>
        public CameraShadowEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="CameraShadowEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public CameraShadowEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static CameraShadowEffect()
        {
            EffectType = new EffectType(
                98, 0x45000000, 5, 0, 0, "シャドー(カメラ制御)",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("光源X", 10, -200000, 200000, 10000),
                    new TrackbarDefinition("光源Y", 10, -200000, 200000, -20000),
                    new TrackbarDefinition("光源Z", 10, -200000, 200000, -20000),
                    new TrackbarDefinition("濃さ", 10, 0, 1000, 400),
                    new TrackbarDefinition("精度", 1, 20, 100, 50),
                },
                System.Array.Empty<CheckboxDefinition>()
            );
        }
    }
}
