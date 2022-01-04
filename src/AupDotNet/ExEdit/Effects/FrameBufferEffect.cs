namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// フレームバッファ
    /// </summary>
    public class FrameBufferEffect : Effect
    {
        /// <summary>
        /// フレームバッファのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>フレームバッファをクリア</summary>
        public bool ClearBuffer
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>
        /// <see cref="FrameBufferEffect"/> のインスタンスを初期化します。
        /// </summary>
        public FrameBufferEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="FrameBufferEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public FrameBufferEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static FrameBufferEffect()
        {
            EffectType = new EffectType(
                5, 0x04000008, 0, 1, 0, "フレームバッファ",
                System.Array.Empty<TrackbarDefinition>(),
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("フレームバッファをクリア", true, 0),
                }
            );
        }
    }
}
