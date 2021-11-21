namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// カメラ制御オプション
    /// </summary>
    public class CameraOptionEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.CameraOption;

        /// <summary>カメラの方を向く</summary>
        public bool Billboard
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>カメラの方を向く(縦横方向のみ)</summary>
        public bool BillboardVH
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>カメラの方を向く(横方向のみ)</summary>
        public bool BillboardH
        {
            get => Checkboxes[2] != 0;
            set => Checkboxes[2] = value ? 1 : 0;
        }

        /// <summary>シャドーの対象から外す</summary>
        public bool NoShadow
        {
            get => Checkboxes[3] != 0;
            set => Checkboxes[3] = value ? 1 : 0;
        }

        public CameraOptionEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public CameraOptionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
