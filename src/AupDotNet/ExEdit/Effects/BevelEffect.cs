namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 凸エッジ
    /// </summary>
    public class BevelEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.Bevel;

        /// <summary>幅</summary>
        public Trackbar Width => Trackbars[0];

        /// <summary>高さ</summary>
        public Trackbar Height => Trackbars[1];

        /// <summary>角度</summary>
        public Trackbar Angle => Trackbars[2];

        public BevelEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public BevelEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
