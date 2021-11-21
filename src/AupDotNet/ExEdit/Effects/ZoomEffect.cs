namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 拡大率(基本効果)
    /// </summary>
    public class ZoomEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.Zoom;

        /// <summary>拡大率</summary>
        public Trackbar Zoom => Trackbars[0];

        public Trackbar X => Trackbars[1];
        public Trackbar Y => Trackbars[2];

        public ZoomEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public ZoomEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
