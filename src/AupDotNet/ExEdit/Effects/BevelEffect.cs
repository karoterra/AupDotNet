namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 凸エッジ
    /// </summary>
    public class BevelEffect : NoExtDataEffect
    {
        public static EffectType EffectType { get; }

        /// <summary>幅</summary>
        public Trackbar Width => Trackbars[0];

        /// <summary>高さ</summary>
        public Trackbar Height => Trackbars[1];

        /// <summary>角度</summary>
        public Trackbar Angle => Trackbars[2];

        public BevelEffect()
            : base(EffectType)
        {
        }

        public BevelEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static BevelEffect()
        {
            EffectType = new EffectType(
                36, 0x04000020, 3, 0, 0, "凸エッジ",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("幅", 1, 0, 100, 4),
                    new TrackbarDefinition("高さ", 100, 0, 300, 100),
                    new TrackbarDefinition("角度", 10, -3600, 3600, -450),
                },
                new CheckboxDefinition[] { }
            );
        }
    }
}
