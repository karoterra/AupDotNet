using System;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// クリッピング＆リサイズ(AviUtl組込みフィルタ)
    /// </summary>
    public class ClipResizeFilterEffect : Effect
    {
        public static EffectType EffectType { get; }

        public Trackbar Top => Trackbars[0];
        public Trackbar Bottom => Trackbars[1];
        public Trackbar Left => Trackbars[2];
        public Trackbar Right => Trackbars[3];

        public byte[] Data { get; } = new byte[EffectType.ExtSize];

        public ClipResizeFilterEffect()
            : base(EffectType)
        {
        }

        public ClipResizeFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public ClipResizeFilterEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            data.CopyTo(Data);
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Data.CopyTo(data, 0);
            return data;
        }

        static ClipResizeFilterEffect()
        {
            EffectType = new EffectType(
                103, 0x02004400, 4, 0, 12, "クリッピング＆リサイズ",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("上", 1, 0, 1024, 0),
                    new TrackbarDefinition("下", 1, 0, 1024, 0),
                    new TrackbarDefinition("左", 1, 0, 1024, 0),
                    new TrackbarDefinition("右", 1, 0, 1024, 0),
                },
                new CheckboxDefinition[] { }
            );
        }
    }
}
