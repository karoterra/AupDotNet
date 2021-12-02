namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// リサイズ(基本効果)
    /// </summary>
    public class ResizeEffect : NoExtDataEffect
    {
        public static EffectType EffectType { get; }

        /// <summary>拡大率</summary>
        public Trackbar Zoom => Trackbars[0];

        public Trackbar X => Trackbars[1];
        public Trackbar Y => Trackbars[2];

        /// <summary>補間なし</summary>
        public bool NoInterpolation
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>ドット数でサイズ指定</summary>
        public bool DotMode
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        public ResizeEffect()
            : base(EffectType)
        {
        }

        public ResizeEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static ResizeEffect()
        {
            EffectType = new EffectType(
                56, 0x04008020, 3, 2, 0, "リサイズ",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("拡大率", 100, 0, 500000, 10000),
                    new TrackbarDefinition("X", 100, 0, 500000, 10000),
                    new TrackbarDefinition("Y", 100, 0, 500000, 10000),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("補間なし", true, 0),
                    new CheckboxDefinition("ドット数でサイズ指定", true, 0),
                }
            );
        }
    }
}
