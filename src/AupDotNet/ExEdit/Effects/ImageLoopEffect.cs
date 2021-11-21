namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 画像ループ
    /// </summary>
    public class ImageLoopEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.ImageLoop;

        /// <summary>横回数</summary>
        public Trackbar NumX => Trackbars[0];

        /// <summary>縦回数</summary>
        public Trackbar NumY => Trackbars[1];

        /// <summary>速度X</summary>
        public Trackbar SpeedX => Trackbars[2];

        /// <summary>速度Y</summary>
        public Trackbar SpeedY => Trackbars[3];

        /// <summary>個別オブジェクト</summary>
        public bool Split
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public ImageLoopEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public ImageLoopEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
