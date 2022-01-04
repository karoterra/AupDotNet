using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ワイプ
    /// </summary>
    public class WipeEffect : Effect
    {
        /// <summary>
        /// ファイル名の最大バイト数。
        /// </summary>
        public readonly int MaxFilenameLength = 256;

        /// <summary>
        /// ワイプのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>イン</summary>
        public Trackbar In => Trackbars[0];

        /// <summary>アウト</summary>
        public Trackbar Out => Trackbars[1];

        /// <summary>ぼかし</summary>
        public Trackbar Blur => Trackbars[2];

        /// <summary>反転(イン)</summary>
        public bool ReverseIn
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>反転(アウト)</summary>
        public bool ReverseOut
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>
        /// ワイプの種類
        /// <list type="bullet">
        ///     <item>0. ワイプ(円)</item>
        ///     <item>1. ワイプ(四角)</item>
        ///     <item>2. ワイプ(時計)</item>
        ///     <item>3. ワイプ(横)</item>
        ///     <item>4. ワイプ(縦)</item>
        /// </list>
        /// </summary>
        public int WipeType { get; set; }

        private string _filename = "";
        /// <summary>
        /// transitionフォルダの画像を選んだ時はファイル名(拡張子無し)がここに入る
        /// </summary>
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
        /// <see cref="WipeEffect"/> のインスタンスを初期化します。
        /// </summary>
        public WipeEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="WipeEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public WipeEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="WipeEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public WipeEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            WipeType = data.Slice(0, 4).ToInt32();
            Filename = data.Slice(4, MaxFilenameLength).ToCleanSjisString();
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            WipeType.ToBytes().CopyTo(data, 0);
            Filename.ToSjisBytes(MaxFilenameLength).CopyTo(data, 4);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("type=");
            writer.WriteLine(WipeType);
            writer.Write("name=");
            writer.WriteLine(Filename);
        }

        static WipeEffect()
        {
            EffectType = new EffectType(
                40, 0x04000420, 3, 3, 260, "ワイプ",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("イン", 100, 0, 1000, 50),
                    new TrackbarDefinition("アウト", 100, 0, 1000, 50),
                    new TrackbarDefinition("ぼかし", 1, 0, 100, 2),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("反転(イン)", true, 0),
                    new CheckboxDefinition("反転(アウト)", true, 0),
                    new CheckboxDefinition("ワイプ(円)", false, 0),
                }
            );
        }
    }
}
