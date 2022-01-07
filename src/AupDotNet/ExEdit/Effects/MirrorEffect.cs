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
        /// <summary>
        /// ミラーのフィルタ効果定義。
        /// </summary>
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
        /// </summary>
        /// <remarks>
        /// <list type="table">
        ///     <listheader>
        ///         <term>値</term>
        ///         <description>映す画像の方向</description>
        ///     </listheader>
        ///     <item><term>0</term><description>上側</description></item>
        ///     <item><term>1</term><description>下側</description></item>
        ///     <item><term>2</term><description>左側</description></item>
        ///     <item><term>3</term><description>右側</description></item>
        /// </list>
        /// </remarks>
        public int Direction { get; set; }

        /// <summary>
        /// <see cref="MirrorEffect"/> のインスタンスを初期化します。
        /// </summary>
        public MirrorEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="MirrorEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public MirrorEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="MirrorEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public MirrorEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Direction = data.Slice(0, 4).ToInt32();
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Direction.ToBytes().CopyTo(data, 0);
            return data;
        }

        /// <inheritdoc/>
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
