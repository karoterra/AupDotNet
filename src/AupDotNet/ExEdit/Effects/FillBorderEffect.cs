namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 縁塗りつぶし(AviUtl組込みフィルタ)
    /// </summary>
    public class FillBorderEffect : Effect
    {
        /// <summary>
        /// 縁塗りつぶしのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>上</summary>
        public Trackbar Top => Trackbars[0];
        
        /// <summary>下</summary>
        public Trackbar Bottom => Trackbars[1];
        
        /// <summary>左</summary>
        public Trackbar Left => Trackbars[2];
        
        /// <summary>右</summary>
        public Trackbar Right => Trackbars[3];

        /// <summary>中央に配置</summary>
        public bool Centering
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>縁の色で塗る</summary>
        public bool EdgeColor
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>
        /// <see cref="FillBorderEffect"/> のインスタンスを初期化します。
        /// </summary>
        public FillBorderEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="FillBorderEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public FillBorderEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static FillBorderEffect()
        {
            EffectType = new EffectType(
                104, 0x02000000, 4, 2, 0, "縁塗りつぶし",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("上", 1, -1024, 1024, 0),
                    new TrackbarDefinition("下", 1, -1024, 1024, 0),
                    new TrackbarDefinition("左", 1, -1024, 1024, 0),
                    new TrackbarDefinition("右", 1, -1024, 1024, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("中央に配置", true, 0),
                    new CheckboxDefinition("縁の色で塗る", true, 0),
                }
            );
        }
    }
}
