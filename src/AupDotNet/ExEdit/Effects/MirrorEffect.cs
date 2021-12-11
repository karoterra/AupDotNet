using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ミラー
    /// </summary>
    public class MirrorEffect : Effect
    {
        public static EffectType EffectType { get; }

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
            : base(EffectType)
        {
        }

        public MirrorEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public MirrorEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Direction = data.Slice(0, 4).ToInt32();
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Direction.ToBytes().CopyTo(data, 0);
            return data;
        }

        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("type=");
            writer.WriteLine(Direction);
        }

        static MirrorEffect()
        {
            EffectType = new EffectType(
                62, 0x04000420, 3, 2, 4, "ミラー",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("透明度", 10, 0, 1000, 0),
                    new TrackbarDefinition("減衰", 10, 0, 5000, 0),
                    new TrackbarDefinition("境目調整", 1, -2000, 2000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("上側", false, 0),
                    new CheckboxDefinition("中心の位置を変更", true, 0),
                }
            );
        }
    }
}
