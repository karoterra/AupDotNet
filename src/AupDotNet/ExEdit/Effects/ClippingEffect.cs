namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// クリッピング
    /// </summary>
    public class ClippingEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.Clipping;

        public Trackbar Top => Trackbars[0];
        public Trackbar Bottom => Trackbars[1];
        public Trackbar Left => Trackbars[2];
        public Trackbar Right => Trackbars[3];

        /// <summary>中心の位置を変更</summary>
        public bool Centering
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public ClippingEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public ClippingEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
