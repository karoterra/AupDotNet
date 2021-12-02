namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 方向ブラー(フィルタオブジェクト)
    /// </summary>
    public class DirectionalBlurFilterEffect : Effect
    {
        public static EffectType EffectType { get; }

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>角度</summary>
        public Trackbar Angle => Trackbars[1];

        public DirectionalBlurFilterEffect()
            : base(EffectType)
        {
        }

        public DirectionalBlurFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static DirectionalBlurFilterEffect()
        {
            EffectType = new EffectType(
                46, 0x44000000, 2, 0, 0, "方向ブラー",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("範囲", 1, 0, 500, 20),
                    new TrackbarDefinition("角度", 10, -36000, 36000, 500),
                },
                new CheckboxDefinition[] { }
            );
        }
    }
}
