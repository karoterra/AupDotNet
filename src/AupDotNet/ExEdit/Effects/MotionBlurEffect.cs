namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// モーションブラー
    /// </summary>
    public class MotionBlurEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.MotionBlur;

        /// <summary>間隔</summary>
        public Trackbar Interval => Trackbars[0];

        /// <summary>分解能</summary>
        public Trackbar Resolution => Trackbars[1];

        /// <summary>残像</summary>
        public bool Afterimage
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>オフスクリーン描画</summary>
        public bool Offscreen
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>出力時に分解能を上げる</summary>
        public bool HighResOutput
        {
            get => Checkboxes[2] != 0;
            set => Checkboxes[2] = value ? 1 : 0;
        }

        public MotionBlurEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public MotionBlurEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
