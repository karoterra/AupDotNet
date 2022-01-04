using System;
using System.Collections.Generic;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 画像ファイル
    /// </summary>
    public class ImageFileEffect : Effect
    {
        /// <summary>
        /// ファイル名の最大バイト数。
        /// </summary>
        public readonly int MaxFilenameLength = 256;

        /// <summary>
        /// 画像ファイルのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>
        /// 拡張データのオフセットアドレス 0x0。
        /// </summary>
        public int Field0x0 { get; set; }

        private string _filename = "";
        /// <summary>参照ファイル</summary>
        /// <remarks>文字列の最大バイト数は <see cref="MaxFilenameLength"/> です。</remarks>
        public string Filename
        {
            get => _filename;
            set
            {
                if (value.GetSjisByteCount() >= MaxFilenameLength)
                {
                    throw new MaxByteCountOfStringException(nameof(Filename), MaxFilenameLength);
                }
                _filename = value;
            }
        }

        /// <summary>
        /// <see cref="ImageFileEffect"/> のインスタンスを初期化します。
        /// </summary>
        public ImageFileEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="ImageFileEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public ImageFileEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="ImageFileEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public ImageFileEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Field0x0 = data.Slice(0, 4).ToInt32();
            Filename = data.Slice(4, MaxFilenameLength).ToCleanSjisString();
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Field0x0.ToBytes().CopyTo(data, 0);
            Filename.ToSjisBytes(MaxFilenameLength).CopyTo(data, 4);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("file=");
            writer.WriteLine(Filename);
        }

        static ImageFileEffect()
        {
            EffectType = new EffectType(
                1, 0x04000408, 0, 1, 260, "画像ファイル",
                new TrackbarDefinition[] {},
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("参照ファイル", false, 0),
                }
            );
        }
    }
}
