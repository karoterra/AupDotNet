namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 反転(フィルタオブジェクト)
    /// </summary>
    public class InversionFilterEffect : Effect
    {
        public static EffectType EffectType { get; }

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

        public InversionFilterEffect()
            : base(EffectType)
        {
        }

        public InversionFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static InversionFilterEffect()
        {
            EffectType = new EffectType(
                61, 0x04000000, 0, 4, 0, "反転",
                new TrackbarDefinition[] { },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("上下反転", true, 0),
                    new CheckboxDefinition("左右反転", true, 0),
                    new CheckboxDefinition("輝度反転", true, 0),
                    new CheckboxDefinition("色相反転", true, 0),
                }
            );
        }
    }
}
