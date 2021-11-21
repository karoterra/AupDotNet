using System;
using System.Drawing;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// エッジ抽出
    /// </summary>
    public class EdgeExtractionEffect : Effect
    {
        private const int Id = (int)EffectTypeId.EdgeExtraction;

        /// <summary>強さ</summary>
        public Trackbar Intensity => Trackbars[0];

        /// <summary>しきい値</summary>
        public Trackbar Threshold => Trackbars[1];

        /// <summary>輝度エッジを抽出</summary>
        public bool LuminanceEdge
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>透明度エッジを抽出</summary>
        public bool TransparencyEdge
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>色の設定</summary>
        public Color Color { get; set; }

        public EdgeExtractionEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public EdgeExtractionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public EdgeExtractionEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    Color = span.Slice(0, 4).ToColor(true);
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
            Color.ToBytes(true).CopyTo(data, 0);
            return data;
        }
    }
}
