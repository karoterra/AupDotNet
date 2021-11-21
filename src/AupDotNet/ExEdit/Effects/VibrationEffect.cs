namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 振動
    /// </summary>
    public class VibrationEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.Vibration;

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

        public VibrationEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public VibrationEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
