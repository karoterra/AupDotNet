using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// パーティクル出力
    /// </summary>
    public class ParticleEffect : Effect
    {
        public static EffectType EffectType { get; }

        public Trackbar X => Trackbars[0];
        public Trackbar Y => Trackbars[1];
        public Trackbar Z => Trackbars[2];

        /// <summary>出力頻度</summary>
        public Trackbar Frequency => Trackbars[3];

        /// <summary>出力速度</summary>
        public Trackbar Speed => Trackbars[4];

        /// <summary>加速度</summary>
        public Trackbar Acceleration => Trackbars[5];

        /// <summary>出力方向</summary>
        public Trackbar Direction => Trackbars[6];

        /// <summary>拡散角度</summary>
        public Trackbar Diffusion => Trackbars[7];

        /// <summary>透過率</summary>
        public Trackbar Alpha => Trackbars[8];

        /// <summary>透過速度</summary>
        public Trackbar Fading => Trackbars[9];

        /// <summary>拡大率</summary>
        public Trackbar Zoom => Trackbars[10];

        /// <summary>拡大速度</summary>
        public Trackbar Expanding => Trackbars[11];

        /// <summary>回転角</summary>
        public Trackbar Rotate => Trackbars[12];

        /// <summary>回転速度</summary>
        public Trackbar Revolution => Trackbars[13];

        /// <summary>重力</summary>
        public Trackbar Gravity => Trackbars[14];

        /// <summary>生存時間</summary>
        public Trackbar Duration => Trackbars[15];

        /// <summary>出力方向の基準を移動方向にする</summary>
        public bool Tail
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>移動範囲の座標からランダムに出力</summary>
        public bool EmittingFromTrace
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>3Dランダム回転</summary>
        public bool RandomRotation
        {
            get => Checkboxes[2] != 0;
            set => Checkboxes[2] = value ? 1 : 0;
        }

        /// <summary>終了点で全て消えるように調節する</summary>
        public bool Adjust
        {
            get => Checkboxes[4] != 0;
            set => Checkboxes[4] = value ? 1 : 0;
        }

        public BlendMode BlendMode { get; set; }

        public ParticleEffect()
            : base(EffectType)
        {
        }

        public ParticleEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public ParticleEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            BlendMode = (BlendMode)data.Slice(0, 4).ToInt32();
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            ((int)BlendMode).ToBytes().CopyTo(data, 0);
            return data;
        }

        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("blend=");
            writer.WriteLine((int)BlendMode);
        }

        static ParticleEffect()
        {
            EffectType = new EffectType(
                13, 0x44000450, 16, 5, 4, "パーティクル出力",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("X", 10, -999999, 999999, 0),
                    new TrackbarDefinition("Y", 10, -999999, 999999, 0),
                    new TrackbarDefinition("Z", 10, -999999, 999999, 0),
                    new TrackbarDefinition("出力頻度", 10, 0, 5000, 200),
                    new TrackbarDefinition("出力速度", 10, 0, 200000, 4000),
                    new TrackbarDefinition("加速度", 10, -200000, 200000, 0),
                    new TrackbarDefinition("出力方向", 10, -36000, 36000, 0),
                    new TrackbarDefinition("拡散角度", 10, 0, 3600, 300),
                    new TrackbarDefinition("透過率", 10, 0, 1000, 0),
                    new TrackbarDefinition("透過速度", 10, -2000, 2000, 0),
                    new TrackbarDefinition("拡大率", 100, 0, 80000, 10000),
                    new TrackbarDefinition("拡大速度", 100, -80000, 80000, 0),
                    new TrackbarDefinition("回転角", 100, -360000, 360000, 0),
                    new TrackbarDefinition("回転速度", 100, -1000, 1000, 0),
                    new TrackbarDefinition("重力", 10, -20000, 20000, 0),
                    new TrackbarDefinition("生存時間", 10, 0, 6000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("出力方向の基準を移動方向にする", true, 0),
                    new CheckboxDefinition("移動範囲の座標からランダムに出力", true, 0),
                    new CheckboxDefinition("3Dランダム回転", true, 0),
                    new CheckboxDefinition("通常", false, 0),
                    new CheckboxDefinition("終了点で全て消えるように調節する", true, 1),
                }
            );
        }
    }
}
