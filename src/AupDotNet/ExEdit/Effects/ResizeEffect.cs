namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// リサイズ(基本効果)
    /// </summary>
    public class ResizeEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.Resize;

        /// <summary>拡大率</summary>
        public Trackbar Zoom => Trackbars[0];

        public Trackbar X => Trackbars[1];
        public Trackbar Y => Trackbars[2];

        /// <summary>補間なし</summary>
        public bool NoInterpolation
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>ドット数でサイズ指定</summary>
        public bool DotMode
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        public ResizeEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public ResizeEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
