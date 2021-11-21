using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// カメラ制御
    /// </summary>
    public class CameraControlEffect : Effect
    {
        private const int Id = (int)EffectTypeId.CameraControl;

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
            : base(EffectType.Defaults[Id])
        {
        }

        public CameraControlEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public CameraControlEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    Range = span.Slice(0, 4).ToInt32();
                    span.Slice(4, 16).CopyTo(Data);
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
            Range.ToBytes().CopyTo(data, 0);
            Data.CopyTo(data, 4);
            return data;
        }
    }
}
