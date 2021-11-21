namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ぼかしフィルタ(AviUtl組込みフィルタ)
    /// </summary>
    public class BlurFilter2Effect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.BlurFilter2;

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        /// <summary>範囲</summary>
        public Trackbar Size => Trackbars[1];

        /// <summary>下限値</summary>
        public Trackbar Lower => Trackbars[2];

        /// <summary>上限値</summary>
        public Trackbar Upper => Trackbars[3];

        public BlurFilter2Effect()
            : base(EffectType.Defaults[Id])
        {
        }

        public BlurFilter2Effect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
