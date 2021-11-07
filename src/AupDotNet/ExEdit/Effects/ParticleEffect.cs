using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// パーティクル出力
    /// </summary>
    public class ParticleEffect : Effect
    {
        private const int Id = (int)EffectTypeId.Particle;

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
            : base(EffectType.Defaults[Id])
        {
        }

        public ParticleEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public ParticleEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
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
    }
}
