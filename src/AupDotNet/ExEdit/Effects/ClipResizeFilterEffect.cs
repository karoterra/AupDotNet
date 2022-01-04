using System;
using System.IO;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// クリッピング＆リサイズ(AviUtl組込みフィルタ)
    /// </summary>
    public class ClipResizeFilterEffect : Effect
    {
        /// <summary>
        /// クリッピング＆リサイズのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>上</summary>
        public Trackbar Top => Trackbars[0];
        
        /// <summary>下</summary>
        public Trackbar Bottom => Trackbars[1];
        
        /// <summary>左</summary>
        public Trackbar Left => Trackbars[2];
        
        /// <summary>右</summary>
        public Trackbar Right => Trackbars[3];

        /// <summary>
        /// 拡張データ。
        /// </summary>
        public byte[] Data { get; } = new byte[EffectType.ExtSize];

        /// <summary>
        /// <see cref="ClipResizeFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        public ClipResizeFilterEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="ClipResizeFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public ClipResizeFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="ClipResizeFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public ClipResizeFilterEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            data.CopyTo(Data);
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Data.CopyTo(data, 0);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("_exdata=");
            writer.WriteLine(ExeditUtil.BytesToString(Data));
        }

        static ClipResizeFilterEffect()
        {
            EffectType = new EffectType(
                103, 0x02004400, 4, 0, 12, "クリッピング＆リサイズ",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("上", 1, 0, 1024, 0),
                    new TrackbarDefinition("下", 1, 0, 1024, 0),
                    new TrackbarDefinition("左", 1, 0, 1024, 0),
                    new TrackbarDefinition("右", 1, 0, 1024, 0),
                },
                Array.Empty<CheckboxDefinition>()
            );
        }
    }
}
