namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 拡散光
    /// </summary>
    public class DiffuseEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.Diffuse;

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        /// <summary>拡散</summary>
        public Trackbar Diffusion => Trackbars[1];

        /// <summary>サイズ固定</summary>
        public bool FixSize
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public DiffuseEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public DiffuseEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
