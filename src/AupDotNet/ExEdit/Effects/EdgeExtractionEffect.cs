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
        public static EffectType EffectType { get; }

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
            : base(EffectType)
        {
        }

        public EdgeExtractionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public EdgeExtractionEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType, trackbars, checkboxes)
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

        static EdgeExtractionEffect()
        {
            EffectType = new EffectType(
                37, 0x04000420, 2, 3, 4, "エッジ抽出",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("強さ", 10, 0, 10000, 1000),
                    new TrackbarDefinition("しきい値", 100, -10000, 10000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("輝度エッジを抽出", true, 1),
                    new CheckboxDefinition("透明度エッジを抽出", true, 0),
                    new CheckboxDefinition("色の設定", false, 0),
                }
            );
        }
    }
}
