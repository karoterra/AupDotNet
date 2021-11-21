namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// シャドー(カメラ制御)
    /// </summary>
    public class CameraShadowEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.CameraShadow;

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

        public CameraShadowEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public CameraShadowEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
