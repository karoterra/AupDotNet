namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 振動(フィルタオブジェクト)
    /// </summary>
    public class VibrationFilterEffect : NoExtDataEffect
    {
        public static EffectType EffectType { get; }

        public Trackbar X => Trackbars[0];
        public Trackbar Y => Trackbars[1];
        public Trackbar Z => Trackbars[2];

        /// <summary>周期</summary>
        public Trackbar Period => Trackbars[3];

        /// <summary>ランダムに強さを変える</summary>
        public bool Random
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>複雑に振動</summary>
        public bool Complex
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        public VibrationFilterEffect()
            : base(EffectType)
        {
        }

        public VibrationFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static VibrationFilterEffect()
        {
            EffectType = new EffectType(
                59, 0x04000000, 4, 2, 0, "振動",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("X", 1, -500, 500, 10),
                    new TrackbarDefinition("Y", 1, -500, 500, 10),
                    new TrackbarDefinition("Z", 1, -500, 500, 0),
                    new TrackbarDefinition("周期", 1, 1, 100, 1),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("ランダムに強さを変える", true, 1),
                    new CheckboxDefinition("複雑に振動", true, 0),
                }
            );
        }
    }
}
