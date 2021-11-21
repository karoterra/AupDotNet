namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 反転(基本効果)
    /// </summary>
    public class InversionEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.Inversion;

        /// <summary>上下反転</summary>
        public bool Vertical
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>左右反転</summary>
        public bool Horizontal
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>輝度反転</summary>
        public bool Luminance
        {
            get => Checkboxes[2] != 0;
            set => Checkboxes[2] = value ? 1 : 0;
        }

        /// <summary>色相反転</summary>
        public bool Hue
        {
            get => Checkboxes[3] != 0;
            set => Checkboxes[3] = value ? 1 : 0;
        }

        /// <summary>透明度反転</summary>
        public bool Transparency
        {
            get => Checkboxes[4] != 0;
            set => Checkboxes[4] = value ? 1 : 0;
        }

        public InversionEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public InversionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
