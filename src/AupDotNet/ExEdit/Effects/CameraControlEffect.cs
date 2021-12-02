using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// カメラ制御
    /// </summary>
    public class CameraControlEffect : Effect
    {
        public static EffectType EffectType { get; }

        /// <summary>X</summary>
        public Trackbar X => Trackbars[0];

        /// <summary>Y</summary>
        public Trackbar Y => Trackbars[1];

        /// <summary>Z</summary>
        public Trackbar Z => Trackbars[2];

        /// <summary>目標X</summary>
        public Trackbar TargetX => Trackbars[3];

        /// <summary>目標Y</summary>
        public Trackbar TargetY => Trackbars[4];

        /// <summary>目標Z</summary>
        public Trackbar TargetZ => Trackbars[5];

        /// <summary>目標レイヤー</summary>
        public Trackbar TargetLayer => Trackbars[6];

        /// <summary>傾き</summary>
        public Trackbar Roll => Trackbars[7];

        /// <summary>深度ぼけ</summary>
        public Trackbar ShallowFocus => Trackbars[8];

        /// <summary>視野角</summary>
        public Trackbar FieldOfView => Trackbars[9];

        /// <summary>Zバッファ/シャドウマップを有効にする</summary>
        public bool EnableZBufferShadow
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>対象レイヤー数</summary>
        public int Range { get; set; }

        public byte[] Data { get; } = new byte[16];

        public CameraControlEffect()
            : base(EffectType)
        {
        }

        public CameraControlEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public CameraControlEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Range = data.Slice(0, 4).ToInt32();
            data.Slice(4, 16).CopyTo(Data);
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Range.ToBytes().CopyTo(data, 0);
            Data.CopyTo(data, 4);
            return data;
        }

        static CameraControlEffect()
        {
            EffectType = new EffectType(
                95, 0x45800400, 10, 1, 20, "カメラ制御",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("X", 10, -999999, 999999, 0),
                    new TrackbarDefinition("Y", 10, -999999, 999999, 0),
                    new TrackbarDefinition("Z", 10, -999999, 999999, -10240),
                    new TrackbarDefinition("目標X", 10, -999999, 999999, 0),
                    new TrackbarDefinition("目標Y", 10, -999999, 999999, 0),
                    new TrackbarDefinition("目標Z", 10, -999999, 999999, 0),
                    new TrackbarDefinition("目標ﾚｲﾔ", 1, 0, 100, 0),
                    new TrackbarDefinition("傾き", 100, -360000, 360000, 0),
                    new TrackbarDefinition("深度ぼけ", 10, 0, 100, 0),
                    new TrackbarDefinition("視野角", 100, 0, 12000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("Zバッファ/シャドウマップを有効にする", true, 1),
                }
            );
        }
    }
}
