namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// カメラ制御オプション
    /// </summary>
    public class CameraOptionEffect : Effect
    {
        /// <summary>
        /// カメラ制御オプションのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

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

        /// <summary>
        /// <see cref="CameraOptionEffect"/> のインスタンスを初期化します。
        /// </summary>
        public CameraOptionEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="CameraOptionEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public CameraOptionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static CameraOptionEffect()
        {
            EffectType = new EffectType(
                85, 0x04000020, 0, 4, 0, "カメラ制御オプション",
                System.Array.Empty<TrackbarDefinition>(),
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("カメラの方を向く", true, 0),
                    new CheckboxDefinition("カメラの方を向く(縦横方向のみ)", true, 0),
                    new CheckboxDefinition("カメラの方を向く(横方向のみ)", true, 0),
                    new CheckboxDefinition("シャドーの対象から外す", true, 0),
                }
            );
        }
    }
}
