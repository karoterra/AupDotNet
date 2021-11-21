namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ローテーション(基本効果)
    /// </summary>
    public class Rotation90Effect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.Rotation90;

        /// <summary>90度回転</summary>
        public Trackbar Rotation => Trackbars[0];

        public Rotation90Effect()
            : base(EffectType.Defaults[Id])
        {
        }

        public Rotation90Effect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
