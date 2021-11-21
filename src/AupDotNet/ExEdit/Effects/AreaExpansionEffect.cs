namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 領域拡張(基本効果)
    /// </summary>
    public class AreaExpansionEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.AreaExpansion;

        public Trackbar Top => Trackbars[0];
        public Trackbar Bottom => Trackbars[1];
        public Trackbar Left => Trackbars[2];
        public Trackbar Right => Trackbars[3];

        /// <summary>塗りつぶし</summary>
        public bool Fill
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public AreaExpansionEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public AreaExpansionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
