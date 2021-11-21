namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// レンズブラー
    /// </summary>
    public class LensBlurEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.LensBlur;

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>光の強さ</summary>
        public Trackbar Intensity => Trackbars[1];

        /// <summary>サイズ固定</summary>
        public bool FixSize
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public LensBlurEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public LensBlurEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
