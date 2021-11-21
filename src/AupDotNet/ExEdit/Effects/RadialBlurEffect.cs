namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 放射ブラー
    /// </summary>
    public class RadialBlurEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.RadialBlur;

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>中心X</summary>
        public Trackbar X => Trackbars[1];

        /// <summary>中心Y</summary>
        public Trackbar Y => Trackbars[2];

        /// <summary>サイズ固定</summary>
        public bool FixSize
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public RadialBlurEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public RadialBlurEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
