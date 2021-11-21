using System;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// クリッピング＆リサイズ(AviUtl組込みフィルタ)
    /// </summary>
    public class ClipResizeFilterEffect : Effect
    {
        private const int Id = (int)EffectTypeId.ClipResizeFilter;

        public Trackbar Top => Trackbars[0];
        public Trackbar Bottom => Trackbars[1];
        public Trackbar Left => Trackbars[2];
        public Trackbar Right => Trackbars[3];

        public byte[] Data { get; } = new byte[EffectType.Defaults[Id].ExtSize];

        public ClipResizeFilterEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public ClipResizeFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public ClipResizeFilterEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    data.CopyTo(Data, 0);
                }
                else if (data.Length != 0)
                {
                    throw new ArgumentException("data's length is invalid.");
                }
            }
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Data.CopyTo(data, 0);
            return data;
        }
    }
}
