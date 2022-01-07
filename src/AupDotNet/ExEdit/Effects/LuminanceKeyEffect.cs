using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ルミナンスキー
    /// </summary>
    public class LuminanceKeyEffect : Effect
    {
        /// <summary>
        /// ルミナンスキーのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>基準輝度</summary>
        public Trackbar Reference => Trackbars[0];

        /// <summary>ぼかし</summary>
        public Trackbar Blur => Trackbars[1];

        /// <summary>
        /// 種類
        /// </summary>
        /// <remarks>
        /// <list type="table">
        ///     <listheader>
        ///         <term>値</term>
        ///         <description>透過させる方法</description>
        ///     </listheader>
        ///     <item><term>0</term><description>暗い部分を透過</description></item>
        ///     <item><term>1</term><description>明るい部分を透過</description></item>
        ///     <item><term>2</term><description>明暗部分を透過</description></item>
        ///     <item><term>3</term><description>明暗部分を透過(ぼかし無し)</description></item>
        /// </list>
        /// </remarks>
        public int TransparentType { get; set; }

        /// <summary>
        /// <see cref="AnimationEffect"/> のインスタンスを初期化します。
        /// </summary>
        public LuminanceKeyEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="AnimationEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public LuminanceKeyEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="AnimationEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public LuminanceKeyEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            TransparentType = data.ToInt32();
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            TransparentType.ToBytes().CopyTo(data, 0);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("type=");
            writer.WriteLine(TransparentType);
        }

        static LuminanceKeyEffect()
        {
            EffectType = new EffectType(
                32, 0x04000420, 2, 1, 4, "ルミナンスキー",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("基準輝度", 1, 0, 8192, 2048),
                    new TrackbarDefinition("ぼかし", 1, 0, 4096, 512),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("暗い部分を透過", false, 0),
                }
            );
        }
    }
}
