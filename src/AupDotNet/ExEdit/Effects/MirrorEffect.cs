using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ミラー
    /// </summary>
    public class MirrorEffect : Effect
    {
        private const int Id = (int)EffectTypeId.Mirror;

        /// <summary>透明度</summary>
        public Trackbar Transparency => Trackbars[0];

        /// <summary>減衰</summary>
        public Trackbar Decay => Trackbars[1];

        /// <summary>境目調整</summary>
        public Trackbar Border => Trackbars[2];

        /// <summary>中心の位置を変更</summary>
        public bool Centering
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>
        /// ミラーの方向
        /// <list type="bullet">
        ///     <item>0. 上側</item>
        ///     <item>1. 下側</item>
        ///     <item>2. 左側</item>
        ///     <item>3. 右側</item>
        /// </list>
        /// </summary>
        public int Direction { get; set; }

        public MirrorEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public MirrorEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public MirrorEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    Direction = span.Slice(0, 4).ToInt32();
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
            Direction.ToBytes().CopyTo(data, 0);
            return data;
        }
    }
}
