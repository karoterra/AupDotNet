namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ぼかし
    /// </summary>
    public class BlurEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.Blur;

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[0];

        /// <summary>縦横比</summary>
        public Trackbar AspectRatio => Trackbars[1];

        /// <summary>光の強さ</summary>
        public Trackbar Intensity => Trackbars[2];

        /// <summary>サイズ固定</summary>
        public bool FixSize
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public BlurEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public BlurEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
