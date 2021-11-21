using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// グループ制御
    /// </summary>
    public class GroupControlEffect : Effect
    {
        private const int Id = (int)EffectTypeId.GroupControl;

        /// <summary>X</summary>
        public Trackbar X => Trackbars[0];

        /// <summary>Y</summary>
        public Trackbar Y => Trackbars[1];

        /// <summary>Z</summary>
        public Trackbar Z => Trackbars[2];

        /// <summary>拡大率</summary>
        public Trackbar Zoom => Trackbars[3];

        /// <summary>X軸回転</summary>
        public Trackbar RotateX => Trackbars[4];

        /// <summary>Y軸回転</summary>
        public Trackbar RotateY => Trackbars[5];

        /// <summary>Z軸回転</summary>
        public Trackbar RotateZ => Trackbars[6];

        /// <summary>上位グループ制御の影響を受ける</summary>
        public bool Inherit
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>同じグループのオブジェクトを対象にする</summary>
        public bool OnlySameGroup
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>対象レイヤー数</summary>
        public int Range { get; set; }

        public byte[] Data { get; } = new byte[16];

        public GroupControlEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public GroupControlEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public GroupControlEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
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
