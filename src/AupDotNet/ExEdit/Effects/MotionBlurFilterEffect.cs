namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// モーションブラー(フィルタオブジェクト)
    /// </summary>
    public class MotionBlurFilterEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.MotionBlurFilter;

        /// <summary>間隔</summary>
        public Trackbar Interval => Trackbars[0];

        /// <summary>分解能</summary>
        public Trackbar Resolution => Trackbars[1];

        /// <summary>出力時に分解能を上げる</summary>
        public bool HighResOutput
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public MotionBlurFilterEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public MotionBlurFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
