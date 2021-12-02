namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 音量の調整(フィルタオブジェクト)
    /// </summary>
    public class VolumeFilterEffect : NoExtDataEffect
    {
        public static EffectType EffectType { get; }

        /// <summary>レベル</summary>
        public Trackbar Level => Trackbars[0];

        public VolumeFilterEffect()
            : base(EffectType)
        {
        }

        public VolumeFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static VolumeFilterEffect()
        {
            EffectType = new EffectType(
                107, 0x02200000, 1, 0, 0, "音量の調整",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("レベル", 1, -256, 256, 0),
                },
                new CheckboxDefinition[] { }
            );
        }
    }
}
