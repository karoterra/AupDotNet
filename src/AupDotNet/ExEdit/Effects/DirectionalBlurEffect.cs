namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 方向ブラー
    /// </summary>
    public class DirectionalBlurEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.DirectionalBlur;

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
            : base(EffectType.Defaults[Id])
        {
        }

        public DirectionalBlurEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
