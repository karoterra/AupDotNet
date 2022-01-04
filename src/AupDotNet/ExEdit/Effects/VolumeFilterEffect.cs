namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 音量の調整(フィルタオブジェクト)
    /// </summary>
    public class VolumeFilterEffect : Effect
    {
        /// <summary>
        /// 音量の調整のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>レベル</summary>
        public Trackbar Level => Trackbars[0];

        /// <summary>
        /// <see cref="VolumeFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        public VolumeFilterEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="VolumeFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
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
