namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// クリッピング
    /// </summary>
    public class ClippingEffect : Effect
    {
        public static EffectType EffectType { get; }

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
            : base(EffectType)
        {
        }

        public ClippingEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static ClippingEffect()
        {
            EffectType = new EffectType(
                17, 0x04000020, 4, 1, 0, "クリッピング",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("上", 1, 0, 4000, 0),
                    new TrackbarDefinition("下", 1, 0, 4000, 0),
                    new TrackbarDefinition("左", 1, 0, 4000, 0),
                    new TrackbarDefinition("右", 1, 0, 4000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("中心の位置を変更", true, 0),
                }
            );
        }
    }
}
