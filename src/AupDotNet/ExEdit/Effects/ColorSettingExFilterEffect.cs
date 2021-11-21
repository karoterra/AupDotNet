namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 拡張色設定(フィルタオブジェクト)
    /// </summary>
    public class ColorSettingExFilterEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.ColorSettingExFilter;

        /// <summary>R, H</summary>
        public Trackbar R => Trackbars[0];

        /// <summary>G, S</summary>
        public Trackbar G => Trackbars[1];

        /// <summary>B, V</summary>
        public Trackbar B => Trackbars[2];

        /// <summary>RGB⇔HSV</summary>
        public bool HSV
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public ColorSettingExFilterEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public ColorSettingExFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
