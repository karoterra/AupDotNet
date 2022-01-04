namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 拡張色調補正(AviUtl組込みフィルタ)
    /// </summary>
    public class ColorCorrectionExEffect : Effect
    {
        /// <summary>
        /// 拡張色調補正のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>Y(offs)</summary>
        public Trackbar OffsetY => Trackbars[0];

        /// <summary>Y(gain)</summary>
        public Trackbar GainY => Trackbars[1];

        /// <summary>Cb(offs)</summary>
        public Trackbar OffsetCb => Trackbars[2];

        /// <summary>Cb(gain)</summary>
        public Trackbar GainCb => Trackbars[3];

        /// <summary>Cr(offs)</summary>
        public Trackbar OffsetCr => Trackbars[4];

        /// <summary>Cr(gain)</summary>
        public Trackbar GainCr => Trackbars[5];

        /// <summary>R(offs)</summary>
        public Trackbar OffsetR => Trackbars[6];

        /// <summary>R(gain)</summary>
        public Trackbar GainR => Trackbars[7];

        /// <summary>R(gamm)</summary>
        public Trackbar GammaR => Trackbars[8];

        /// <summary>G(offs)</summary>
        public Trackbar OffsetG => Trackbars[9];

        /// <summary>G(gain)</summary>
        public Trackbar GainG => Trackbars[10];

        /// <summary>G(gamm)</summary>
        public Trackbar GammaG => Trackbars[11];

        /// <summary>B(offs)</summary>
        public Trackbar OffsetB => Trackbars[12];

        /// <summary>B(gain)</summary>
        public Trackbar GainB => Trackbars[13];

        /// <summary>B(gamm)</summary>
        public Trackbar GammaB => Trackbars[14];

        /// <summary>RGBの同期</summary>
        public bool SyncRGB
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>TV -> PC スケール補正</summary>
        public bool TVtoPC
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>PC -> TV スケール補正</summary>
        public bool PCtoTV
        {
            get => Checkboxes[2] != 0;
            set => Checkboxes[2] = value ? 1 : 0;
        }

        /// <summary>
        /// <see cref="ColorCorrectionExEffect"/> のインスタンスを初期化します。
        /// </summary>
        public ColorCorrectionExEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="ColorCorrectionExEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public ColorCorrectionExEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static ColorCorrectionExEffect()
        {
            EffectType = new EffectType(
                106, 0x02001000, 15, 3, 0, "拡張色調補正",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("Y(offs)", 1, -256, 256, 0),
                    new TrackbarDefinition("Y(gain)", 1, -256, 256, 0),
                    new TrackbarDefinition("Cb(offs)", 1, -256, 256, 0),
                    new TrackbarDefinition("Cb(gain)", 1, -256, 256, 0),
                    new TrackbarDefinition("Cr(offs)", 1, -256, 256, 0),
                    new TrackbarDefinition("Cr(gain)", 1, -256, 256, 0),
                    new TrackbarDefinition("R(offs)", 1, -256, 256, 0),
                    new TrackbarDefinition("R(gain)", 1, -256, 256, 0),
                    new TrackbarDefinition("R(gamm)", 1, -256, 256, 0),
                    new TrackbarDefinition("G(offs)", 1, -256, 256, 0),
                    new TrackbarDefinition("G(gain)", 1, -256, 256, 0),
                    new TrackbarDefinition("G(gamm)", 1, -256, 256, 0),
                    new TrackbarDefinition("B(offs)", 1, -256, 256, 0),
                    new TrackbarDefinition("B(gain)", 1, -256, 256, 0),
                    new TrackbarDefinition("B(gamm)", 1, -256, 256, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("RGBの同期", true, 0),
                    new CheckboxDefinition("TV -> PC スケール補正", true, 0),
                    new CheckboxDefinition("PC -> TV スケール補正", true, 0),
                }
            );
        }
    }
}
