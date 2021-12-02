namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 方向ブラー
    /// </summary>
    public class DirectionalBlurEffect : NoExtDataEffect
    {
        public static EffectType EffectType { get; }

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>角度</summary>
        public Trackbar Angle => Trackbars[1];

        /// <summary>サイズ固定</summary>
        public bool FixSize
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public DirectionalBlurEffect()
            : base(EffectType)
        {
        }

        public DirectionalBlurEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static DirectionalBlurEffect()
        {
            EffectType = new EffectType(
                45, 0x04000020, 2, 1, 0, "方向ブラー",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("範囲", 1, 0, 500, 20),
                    new TrackbarDefinition("角度", 10, -36000, 36000, 500),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("サイズ固定", true, 0),
                }
            );
        }
    }
}
