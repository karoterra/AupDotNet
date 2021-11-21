namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 縁塗りつぶし(AviUtl組込みフィルタ)
    /// </summary>
    public class FillBorderEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.FillBorder;

        public Trackbar Top => Trackbars[0];
        public Trackbar Bottom => Trackbars[1];
        public Trackbar Left => Trackbars[2];
        public Trackbar Right => Trackbars[3];

        /// <summary>中央に配置</summary>
        public bool Centering
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>縁の色で塗る</summary>
        public bool EdgeColor
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        public FillBorderEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public FillBorderEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
