namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 音量の調整(フィルタオブジェクト)
    /// </summary>
    public class VolumeFilterEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.VolumeFilter;

        /// <summary>レベル</summary>
        public Trackbar Level => Trackbars[0];

        public VolumeFilterEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public VolumeFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
