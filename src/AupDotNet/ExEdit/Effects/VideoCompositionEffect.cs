using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 動画ファイル合成
    /// </summary>
    public class VideoCompositionEffect : Effect
    {
        /// <summary>
        /// ファイル名の最大バイト数。
        /// </summary>
        public static readonly int MaxFilenameLength = 260;

        /// <summary>
        /// 動画ファイル合成のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>再生範囲,オフセット</summary>
        public Trackbar Position => Trackbars[0];

        /// <summary>再生速度</summary>
        public Trackbar Speed => Trackbars[1];

        /// <summary>X</summary>
        public Trackbar X => Trackbars[2];

        /// <summary>Y</summary>
        public Trackbar Y => Trackbars[3];

        /// <summary>拡大率</summary>
        public Trackbar Zoom => Trackbars[4];

        /// <summary>ループ再生</summary>
        public bool LoopPlayback
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>動画ファイルの同期</summary>
        public bool Sync
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>ループ画像</summary>
        public bool LoopImage
        {
            get => Checkboxes[2] != 0;
            set => Checkboxes[2] = value ? 1 : 0;
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
        /// 拡張データのオフセットアドレス 0x104 から 20 バイト分のデータ。
        /// </summary>
        public byte[] Field0x104 { get; } = new byte[20];

        /// <summary>
        /// 合成モード
        /// </summary>
        /// <remarks>
        /// <list type="table">
        ///     <listheader>
        ///         <term>値</term>
        ///         <description>合成モード</description>
        ///     </listheader>
        ///     <item><term>0</term><description>色情報を上書き</description></item>
        ///     <item><term>1</term><description>輝度をアルファ値として上書き</description></item>
        ///     <item><term>2</term><description>輝度をアルファ値として乗算</description></item>
        /// </list>
        /// </remarks>
        public int Mode { get; set; }

        /// <summary>
        /// <see cref="VideoCompositionEffect"/> のインスタンスを初期化します。
        /// </summary>
        public VideoCompositionEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="VideoCompositionEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public VideoCompositionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="VideoCompositionEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public VideoCompositionEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Filename = data.Slice(0, MaxFilenameLength).ToCleanSjisString();
            data.Slice(0x104, 20).CopyTo(Field0x104);
            Mode = data.Slice(0x118, 4).ToInt32();
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Filename.ToSjisBytes(MaxFilenameLength).CopyTo(data, 0);
            Field0x104.CopyTo(data, 0x104);
            Mode.ToBytes().CopyTo(data, 0x118);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("file=");
            writer.WriteLine(Filename);
            writer.Write("mode=");
            writer.WriteLine(Mode);
        }

        static VideoCompositionEffect()
        {
            EffectType = new EffectType(
                82, 0x04000420, 5, 5, 284, "動画ファイル合成",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("再生位置", 1, 1, 1, 1),
                    new TrackbarDefinition("再生速度", 10, -20000, 20000, 1000),
                    new TrackbarDefinition("X", 1, -2000, 2000, 0),
                    new TrackbarDefinition("Y", 1, -2000, 2000, 0),
                    new TrackbarDefinition("拡大率", 10, 0, 8000, 1000),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("ループ再生", true, 0),
                    new CheckboxDefinition("動画ファイルの同期", true, 0),
                    new CheckboxDefinition("ループ画像", true, 0),
                    new CheckboxDefinition("参照ファイル", false, 0),
                    new CheckboxDefinition("色情報を上書き", false, 0),
                }
            );
        }
    }
}
