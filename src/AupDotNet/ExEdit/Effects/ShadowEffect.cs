using System;
using System.Drawing;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// シャドー
    /// </summary>
    public class ShadowEffect : Effect
    {
        /// <summary>
        /// ファイル名の最大バイト数。
        /// </summary>
        public static readonly int MaxFilenameLength = 256;

        /// <summary>
        /// シャドーのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>X</summary>
        public Trackbar X => Trackbars[0];

        /// <summary>Y</summary>
        public Trackbar Y => Trackbars[1];

        /// <summary>濃さ</summary>
        public Trackbar Depth => Trackbars[2];

        /// <summary>拡散</summary>
        public Trackbar Diffusion => Trackbars[3];

        /// <summary>影を別オブジェクトで描画</summary>
        public bool Split
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>影色の設定</summary>
        public Color Color { get; set; } = Color.Black;

        private string _filename = "";
        /// <summary>パターン画像ファイル</summary>
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
        /// <see cref="ShadowEffect"/> のインスタンスを初期化します。
        /// </summary>
        public ShadowEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="ShadowEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public ShadowEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="ShadowEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public ShadowEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Color = data.Slice(0, 4).ToColor(true);
            Filename = data.Slice(4, MaxFilenameLength).ToCleanSjisString();
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Color.ToBytes(true).CopyTo(data, 0);
            Filename.ToSjisBytes(MaxFilenameLength).CopyTo(data, 4);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("color=");
            writer.WriteLine(ExeditUtil.ColorToString(Color));
            writer.Write("file=");
            writer.WriteLine(Filename);
        }

        static ShadowEffect()
        {
            EffectType = new EffectType(
                34, 0x44000420, 4, 3, 260, "シャドー",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("X", 1, -1000, 1000, -40),
                    new TrackbarDefinition("Y", 1, -1000, 1000, 24),
                    new TrackbarDefinition("濃さ", 10, 0, 1000, 400),
                    new TrackbarDefinition("拡散", 1, 0, 500, 10),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("影を別オブジェクトで描画", true, 0),
                    new CheckboxDefinition("影色の設定", false, 0),
                    new CheckboxDefinition("パターン画像ファイル", false, 0),
                }
            );
        }
    }
}
