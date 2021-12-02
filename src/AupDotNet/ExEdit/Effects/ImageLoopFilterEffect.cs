namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 画像ループ(フィルタオブジェクト)
    /// </summary>
    public class ImageLoopFilterEffect : Effect
    {
        public static EffectType EffectType { get; }

        /// <summary>横回数</summary>
        public Trackbar NumX => Trackbars[0];

        /// <summary>縦回数</summary>
        public Trackbar NumY => Trackbars[1];

        /// <summary>速度X</summary>
        public Trackbar SpeedX => Trackbars[2];

        /// <summary>速度Y</summary>
        public Trackbar SpeedY => Trackbars[3];

        public ImageLoopFilterEffect()
            : base(EffectType)
        {
        }

        public ImageLoopFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static ImageLoopFilterEffect()
        {
            EffectType = new EffectType(
                67, 0x04000000, 4, 0, 0, "画像ループ",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("横回数", 1, 1, 400, 1),
                    new TrackbarDefinition("縦回数", 1, 1, 400, 1),
                    new TrackbarDefinition("速度X", 10, -10000, 10000, 0),
                    new TrackbarDefinition("速度Y", 10, -10000, 10000, 0),
                },
                new CheckboxDefinition[] { }
            );
        }
    }
}
