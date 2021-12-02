namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 拡散光
    /// </summary>
    public class DiffuseEffect : Effect
    {
        public static EffectType EffectType { get; }

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
            : base(EffectType)
        {
        }

        public DiffuseEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static DiffuseEffect()
        {
            EffectType = new EffectType(
                26, 0x04000020, 2, 1, 0, "拡散光",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("強さ", 10, 0, 1000, 500),
                    new TrackbarDefinition("拡散", 1, 0, 500, 12),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("サイズ固定", true, 0),
                }
            );
        }
    }
}
