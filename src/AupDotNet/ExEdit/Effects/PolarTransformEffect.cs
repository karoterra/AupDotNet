namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 極座標変換
    /// </summary>
    public class PolarTransformEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.PolarTransform;

        /// <summary>中心幅</summary>
        public Trackbar Margin => Trackbars[0];

        /// <summary>拡大率</summary>
        public Trackbar Zoom => Trackbars[1];

        /// <summary>回転</summary>
        public Trackbar Rotation => Trackbars[2];

        /// <summary>渦巻</summary>
        public Trackbar Spiral => Trackbars[3];

        public PolarTransformEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public PolarTransformEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
