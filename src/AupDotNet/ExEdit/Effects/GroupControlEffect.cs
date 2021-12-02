using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// グループ制御
    /// </summary>
    public class GroupControlEffect : Effect
    {
        public static EffectType EffectType { get; }

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
            : base(EffectType)
        {
        }

        public GroupControlEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public GroupControlEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType, trackbars, checkboxes)
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

        static GroupControlEffect()
        {
            EffectType = new EffectType(
                94, 0x45000420, 7, 2, 20, "グループ制御",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("X", 10, -999999, 999999, 0),
                    new TrackbarDefinition("Y", 10, -999999, 999999, 0),
                    new TrackbarDefinition("Z", 10, -999999, 999999, 0),
                    new TrackbarDefinition("拡大率", 100, 0, 500000, 10000),
                    new TrackbarDefinition("X軸回転", 100, -360000, 360000, 0),
                    new TrackbarDefinition("Y軸回転", 100, -360000, 360000, 0),
                    new TrackbarDefinition("Z軸回転", 100, -360000, 360000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("上位グループ制御の影響を受ける", true, 0),
                    new CheckboxDefinition("同じグループのオブジェクトを対象にする", true, 1),
                }
            );
        }
    }
}
