namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 拡散光(フィルタオブジェクト)
    /// </summary>
    public class DiffuseFilterEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.DiffuseFilter;

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        /// <summary>拡散</summary>
        public Trackbar Diffusion => Trackbars[1];

        public DiffuseFilterEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public DiffuseFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
