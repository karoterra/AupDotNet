namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ラスター(フィルタオブジェクト)
    /// </summary>
    public class RasterFilterEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.RasterFilter;

        /// <summary>横幅</summary>
        public Trackbar Width => Trackbars[0];

        /// <summary>高さ</summary>
        public Trackbar Height => Trackbars[1];

        /// <summary>周期</summary>
        public Trackbar Period => Trackbars[2];

        /// <summary>縦ラスター</summary>
        public bool Vertical
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>ランダム振幅</summary>
        public bool Random
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        public RasterFilterEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public RasterFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
