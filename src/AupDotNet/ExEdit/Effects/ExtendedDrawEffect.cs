using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 拡張描画
    /// </summary>
    public class ExtendedDrawEffect : Effect
    {
        public static EffectType EffectType { get; }

        public Trackbar X => Trackbars[0];
        public Trackbar Y => Trackbars[1];
        public Trackbar Z => Trackbars[2];
        public Trackbar Zoom => Trackbars[3];
        public Trackbar Alpha => Trackbars[4];
        public Trackbar AspectRatio => Trackbars[5];
        public Trackbar RotateX => Trackbars[6];
        public Trackbar RotateY => Trackbars[7];
        public Trackbar RotateZ => Trackbars[8];
        public Trackbar CenterX => Trackbars[9];
        public Trackbar CenterY => Trackbars[10];
        public Trackbar CenterZ => Trackbars[11];

        public bool BackfaceCulling
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        public BlendMode BlendMode { get; set; }

        public ExtendedDrawEffect()
            : base(EffectType)
        {
        }

        public ExtendedDrawEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public ExtendedDrawEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType, trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    BlendMode = (BlendMode)span.Slice(0, 4).ToInt32();
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
            ((int)BlendMode).ToBytes().CopyTo(data, 0);
            return data;
        }

        static ExtendedDrawEffect()
        {
            EffectType = new EffectType(
                11, 0x440004D0, 12, 2, 4, "拡張描画",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("X", 10, -999999, 999999, 0),
                    new TrackbarDefinition("Y", 10, -999999, 999999, 0),
                    new TrackbarDefinition("Z", 10, -999999, 999999, 0),
                    new TrackbarDefinition("拡大率", 100, 0, 500000, 10000),
                    new TrackbarDefinition("透明度", 10, 0, 1000, 0),
                    new TrackbarDefinition("縦横比", 10, -1000, 1000, 0),
                    new TrackbarDefinition("X軸回転", 100, -360000, 360000, 0),
                    new TrackbarDefinition("Y軸回転", 100, -360000, 360000, 0),
                    new TrackbarDefinition("Z軸回転", 100, -360000, 360000, 0),
                    new TrackbarDefinition("中心X", 10, -20000, 20000, 0),
                    new TrackbarDefinition("中心Y", 10, -20000, 20000, 0),
                    new TrackbarDefinition("中心Z", 10, -20000, 20000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("通常", false, 0),
                    new CheckboxDefinition("裏面を表示しない", true, 0),
                }
            );
        }
    }
}
