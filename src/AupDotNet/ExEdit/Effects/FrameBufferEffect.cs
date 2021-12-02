namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// フレームバッファ
    /// </summary>
    public class FrameBufferEffect : NoExtDataEffect
    {
        public static EffectType EffectType { get; }

        /// <summary>フレームバッファをクリア</summary>
        public bool ClearBuffer
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public FrameBufferEffect()
            : base(EffectType)
        {
        }

        public FrameBufferEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static FrameBufferEffect()
        {
            EffectType = new EffectType(
                5, 0x04000008, 0, 1, 0, "フレームバッファ",
                new TrackbarDefinition[] { },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("フレームバッファをクリア", true, 0),
                }
            );
        }
    }
}
