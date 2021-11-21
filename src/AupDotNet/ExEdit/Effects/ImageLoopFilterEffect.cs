namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 画像ループ(フィルタオブジェクト)
    /// </summary>
    public class ImageLoopFilterEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.ImageLoopFilter;

        /// <summary>横回数</summary>
        public Trackbar NumX => Trackbars[0];

        /// <summary>縦回数</summary>
        public Trackbar NumY => Trackbars[1];

        /// <summary>速度X</summary>
        public Trackbar SpeedX => Trackbars[2];

        /// <summary>速度Y</summary>
        public Trackbar SpeedY => Trackbars[3];

        public ImageLoopFilterEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public ImageLoopFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
