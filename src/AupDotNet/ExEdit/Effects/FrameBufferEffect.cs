namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// フレームバッファ
    /// </summary>
    public class FrameBufferEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.FrameBuffer;

        /// <summary>フレームバッファをクリア</summary>
        public bool ClearBuffer
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public FrameBufferEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public FrameBufferEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
