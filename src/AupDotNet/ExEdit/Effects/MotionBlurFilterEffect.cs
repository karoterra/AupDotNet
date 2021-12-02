namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// モーションブラー(フィルタオブジェクト)
    /// </summary>
    public class MotionBlurFilterEffect : NoExtDataEffect
    {
        public static EffectType EffectType { get; }

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
            : base(EffectType)
        {
        }

        public MotionBlurFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static MotionBlurFilterEffect()
        {
            EffectType = new EffectType(
                50, 0x04000000, 2, 1, 0, "モーションブラー",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("間隔", 1, 0, 100, 1),
                    new TrackbarDefinition("分解能", 1, 1, 25, 10),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("出力時に分解能を上げる", true, 0),
                }
            );
        }
    }
}
