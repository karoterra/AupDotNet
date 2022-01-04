using System;
using System.Drawing;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 音声波形表示
    /// </summary>
    public class WaveformEffect : Effect
    {
        /// <summary>
        /// ファイル名の最大バイト数。
        /// </summary>
        public readonly int MaxFilenameLength = 260;

        /// <summary>
        /// 音声波形表示のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>横幅</summary>
        public Trackbar Width => Trackbars[0];

        /// <summary>高さ</summary>
        public Trackbar Height => Trackbars[1];

        /// <summary>音量</summary>
        public Trackbar Volume => Trackbars[2];

        /// <summary>再生位置</summary>
        public Trackbar Position => Trackbars[3];

        /// <summary>編集全体の音声を元にする</summary>
        public bool ReferScene
        {
            get => Checkboxes[3] != 0;
            set => Checkboxes[3] = value ? 1 : 0;
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
        /// 拡張データのオフセットアドレス 0x104 から 20 バイト分。
        /// </summary>
        public byte[] Field0x104 { get; } = new byte[20];

        /// <summary>波形の種類</summary>
        public short PresetType { get; set; }

        /// <summary>波形タイプ(0/1)</summary>
        public short Mode { get; set; }

        /// <summary>横解像度</summary>
        public short ResW { get; set; }

        /// <summary>縦解像度</summary>
        public short ResH { get; set; }

        /// <summary>横スペース(%)</summary>
        public short PadW { get; set; }

        /// <summary>縦スペース(%)</summary>
        public short PadH { get; set; }

        /// <summary>波形タイプ(0/1)</summary>
        public Color Color { get; set; }

        /// <summary>sample_n</summary>
        public int SampleN { get; set; }

        /// <summary>ミラー</summary>
        public bool Mirror { get; set; }

        /// <summary>
        /// <see cref="WaveformEffect"/> のインスタンスを初期化します。
        /// </summary>
        public WaveformEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="WaveformEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public WaveformEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="WaveformEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public WaveformEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Filename = data.Slice(0, MaxFilenameLength).ToCleanSjisString();
            data.Slice(0x104, 20).CopyTo(Field0x104);
            PresetType = data.Slice(0x118, 2).ToInt16();
            Mode = data.Slice(0x11A, 2).ToInt16();
            ResW = data.Slice(0x11C, 2).ToInt16();
            ResH = data.Slice(0x11E, 2).ToInt16();
            PadW = data.Slice(0x120, 2).ToInt16();
            PadH = data.Slice(0x122, 2).ToInt16();
            Color = data.Slice(0x124, 4).ToColor();
            SampleN = data.Slice(0x128, 4).ToInt32();
            Mirror = data[0x12C] != 0;
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Filename.ToSjisBytes(MaxFilenameLength).CopyTo(data, 0);
            Field0x104.CopyTo(data, 0x104);
            PresetType.ToBytes().CopyTo(data, 0x118);
            Mode.ToBytes().CopyTo(data, 0x11A);
            ResW.ToBytes().CopyTo(data, 0x11C);
            ResH.ToBytes().CopyTo(data, 0x11E);
            PadW.ToBytes().CopyTo(data, 0x120);
            PadH.ToBytes().CopyTo(data, 0x122);
            Color.ToBytes().CopyTo(data, 0x124);
            SampleN.ToBytes().CopyTo(data, 0x128);
            data[0x12C] = Mirror ? (byte)1 : (byte)0;
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("file=");
            writer.WriteLine(Filename);
            writer.Write("type=");
            writer.WriteLine(PresetType);
            writer.Write("mode=");
            writer.WriteLine(Mode);
            writer.Write("res_w=");
            writer.WriteLine(ResW);
            writer.Write("res_h=");
            writer.WriteLine(ResH);
            writer.Write("pad_w=");
            writer.WriteLine(PadW);
            writer.Write("pad_h=");
            writer.WriteLine(PadH);
            writer.Write("color=");
            writer.WriteLine(ExeditUtil.ColorToString(Color));
            writer.Write("sample_n=");
            writer.WriteLine(SampleN);
            writer.Write("mirror=");
            writer.WriteLine(Mirror ? '1' : '0');
        }

        static WaveformEffect()
        {
            EffectType = new EffectType(
                6, 0x04000408, 4, 5, 304, "音声波形表示",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("横幅", 1, 0, 2000, 640),
                    new TrackbarDefinition("高さ", 1, 0, 2000, 240),
                    new TrackbarDefinition("音量", 10, 0, 5000, 1000),
                    new TrackbarDefinition("再生位置", 100, 0, 0, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("Type1", false, 0),
                    new CheckboxDefinition("波形の色", false, 0),
                    new CheckboxDefinition("参照ファイル", false, 0),
                    new CheckboxDefinition("編集全体の音声を元にする", true, 1),
                    new CheckboxDefinition("設定", false, 0),
                }
            );
        }
    }
}
