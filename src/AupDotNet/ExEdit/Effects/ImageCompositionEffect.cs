using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 画像ファイル合成
    /// </summary>
    public class ImageCompositionEffect : Effect
    {
        /// <summary>
        /// ファイル名の最大バイト数。
        /// </summary>
        public static readonly int MaxFilenameLength = 256;

        /// <summary>
        /// 画像ファイル合成のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>X</summary>
        public Trackbar X => Trackbars[0];

        /// <summary>Y</summary>
        public Trackbar Y => Trackbars[1];
        
        /// <summary>拡大率</summary>
        public Trackbar Zoom => Trackbars[2];

        /// <summary>ループ画像</summary>
        public bool Loop
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

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
        /// 合成モード
        /// </summary>
        /// <remarks>
        /// <list type="table">
        ///     <listheader>
        ///         <term>値</term>
        ///         <description>合成モード</description>
        ///     </listheader>
        ///     <item><term>0</term><description>前方から合成</description></item>
        ///     <item><term>1</term><description>後方から合成</description></item>
        ///     <item><term>2</term><description>色情報を上書き</description></item>
        ///     <item><term>3</term><description>輝度をアルファ値として上書き</description></item>
        ///     <item><term>4</term><description>輝度をアルファ値として乗算</description></item>
        /// </list>
        /// </remarks>
        public int Mode { get; set; }

        /// <summary>
        /// <see cref="ImageCompositionEffect"/> のインスタンスを初期化します。
        /// </summary>
        public ImageCompositionEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="ImageCompositionEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public ImageCompositionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="ImageCompositionEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public ImageCompositionEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Mode = data.Slice(0, 4).ToInt32();
            Filename = data.Slice(4, MaxFilenameLength).ToCleanSjisString();
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Mode.ToBytes().CopyTo(data, 0);
            Filename.ToSjisBytes(MaxFilenameLength).CopyTo(data, 4);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("mode=");
            writer.WriteLine(Mode);
            writer.Write("file=");
            writer.WriteLine(Filename);
        }

        static ImageCompositionEffect()
        {
            EffectType = new EffectType(
                83, 0x04000420, 3, 3, 260, "画像ファイル合成",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("X", 1, -1000, 1000, 0),
                    new TrackbarDefinition("Y", 1, -1000, 1000, 0),
                    new TrackbarDefinition("拡大率", 10, 0, 8000, 1000),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("ループ画像", true, 0),
                    new CheckboxDefinition("参照ファイル", false, 0),
                    new CheckboxDefinition("前方から合成", false, 0),
                }
            );
        }
    }
}
