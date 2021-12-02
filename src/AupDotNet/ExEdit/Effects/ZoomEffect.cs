namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 拡大率(基本効果)
    /// </summary>
    public class ZoomEffect : NoExtDataEffect
    {
        public static EffectType EffectType { get; }

        /// <summary>拡大率</summary>
        public Trackbar Zoom => Trackbars[0];

        public Trackbar X => Trackbars[1];
        public Trackbar Y => Trackbars[2];

        public ZoomEffect()
            : base(EffectType)
        {
        }

        public ZoomEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static ZoomEffect()
        {
            EffectType = new EffectType(
                52, 0x04008020, 3, 0, 0, "拡大率",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("拡大率", 100, 0, 500000, 10000),
                    new TrackbarDefinition("X", 100, 0, 500000, 10000),
                    new TrackbarDefinition("Y", 100, 0, 500000, 10000),
                },
                new CheckboxDefinition[] { }
            );
        }
    }
}
