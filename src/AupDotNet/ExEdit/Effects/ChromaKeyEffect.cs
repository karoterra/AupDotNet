using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// クロマキー
    /// </summary>
    public class ChromaKeyEffect : Effect
    {
        /// <summary>
        /// クロマキーのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>色相範囲</summary>
        public Trackbar HueRange => Trackbars[0];

        /// <summary>彩度範囲</summary>
        public Trackbar ChromaRange => Trackbars[1];

        /// <summary>境界補正</summary>
        public Trackbar Boundary => Trackbars[2];

        /// <summary>色彩補正</summary>
        public bool ColorCorrection
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>透過補正</summary>
        public bool TransparencyCorrection
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>キー色の取得</summary>
        public YCbCr Color { get; set; }

        /// <summary>キー(未取得)</summary>
        public int Status { get; set; }

        /// <summary>
        /// 拡張データのオフセットアドレス 0x6。
        /// </summary>
        public short Field0x6 { get; set; }

        /// <summary>
        /// <see cref="ChromaKeyEffect"/> のインスタンスを初期化します。
        /// </summary>
        public ChromaKeyEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="ChromaKeyEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public ChromaKeyEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="ChromaKeyEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public ChromaKeyEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Color = data.ToYCbCr();
            Field0x6 = data.Slice(6, 2).ToInt16();
            Status = data.Slice(8, 4).ToInt32();
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Color.ToBytes().CopyTo(data, 0);
            Field0x6.ToBytes().CopyTo(data, 6);
            Status.ToBytes().CopyTo(data, 8);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("color_yc=");
            writer.WriteLine(Color.ToString());
            writer.Write("status=");
            writer.WriteLine(Status);
        }

        static ChromaKeyEffect()
        {
            EffectType = new EffectType(
                30, 0x04000420, 3, 3, 12, "クロマキー",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("色相範囲", 1, 0, 256, 24),
                    new TrackbarDefinition("彩度範囲", 1, 0, 256, 96),
                    new TrackbarDefinition("境界補正", 1, 0, 5, 1),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("色彩補正", true, 0),
                    new CheckboxDefinition("透過補正", true, 0),
                    new CheckboxDefinition("キー色の取得", false, 0),
                }
            );
        }
    }
}
