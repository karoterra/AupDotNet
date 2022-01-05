using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 部分フィルタ(フィルタオブジェクト)
    /// </summary>
    public class PartialFilterEffect : Effect
    {
        /// <summary>
        /// マスクの名前の最大バイト数。
        /// </summary>
        public static readonly int MaxNameLength = 256;

        /// <summary>
        /// 部分フィルタのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>X</summary>
        public Trackbar X => Trackbars[0];
        
        /// <summary>Y</summary>
        public Trackbar Y => Trackbars[1];

        /// <summary>回転</summary>
        public Trackbar Rotate => Trackbars[2];
        
        /// <summary>サイズ</summary>
        public Trackbar Size => Trackbars[3];

        /// <summary>縦横比</summary>
        public Trackbar AspectRatio => Trackbars[4];

        /// <summary>ぼかし</summary>
        public Trackbar Blur => Trackbars[5];

        /// <summary>マスクの反転</summary>
        public bool Invert
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>
        /// 使用している組込み図形の種類
        /// </summary>
        /// <remarks>
        /// 組込み図形以外のマスクを使用している場合は <see cref="Name"/> にその情報が格納されます。
        /// </remarks>
        /// <seealso cref="NameType"/>
        public FigureType FigureType { get; set; } = FigureType.Square;

        private string _name = "";
        /// <summary>
        /// マスクの名前
        /// </summary>
        /// <remarks>
        /// 組込み図形以外のマスクを使用している場合はここにその情報が格納されます。
        /// 文字列の最大バイト数は <see cref="MaxNameLength"/> です。
        /// <see cref="NameType"/> が <see cref="FigureNameType.Figure"/> の場合は figure フォルダの画像ファイル名(拡張子無し)がここに入ります。
        /// </remarks>
        /// <seealso cref="NameType"/>
        public string Name
        {
            get => _name;
            set
            {
                if (value.GetSjisByteCount() >= MaxNameLength)
                {
                    throw new MaxByteCountOfStringException(nameof(Name), MaxNameLength);
                }
                _name = value;
            }
        }

        /// <summary>
        /// マスクの種類
        /// </summary>
        /// <remarks>
        /// 組込み図形以外のマスクを使用している場合は <see cref="Name"/> にその情報が格納されます。
        /// <c>NameType</c> は <c>Name</c> がどのような種類のマスクの情報を格納しているかを表します。
        /// <list type="table">
        ///     <listheader>
        ///         <term>値</term>
        ///         <description>説明</description>
        ///     </listheader>
        ///     <item>
        ///         <term><see cref="FigureNameType.BuiltIn"/></term>
        ///         <description>
        ///             組込み図形が選択されています。
        ///             <see cref="Name"/> は <c>null</c> または空です。
        ///             <see cref="FigureType"/> を参照してください。
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="FigureNameType.Figure"/></term>
        ///         <description>
        ///             figureフォルダの画像が選択されています。
        ///             <see cref="Name"/> には画像のファイル名(拡張子無し)が入っています。
        ///         </description>
        ///     </item>
        ///     <item>
        ///         <term><see cref="FigureNameType.File"/></term>
        ///         <description>
        ///             外部ファイルが参照されています。
        ///             <see cref="Name"/> には画像ファイルの絶対パスの先頭に <c>*</c> が付いたものが入っています。
        ///             画像ファイルのパスを取得する場合は <see cref="Filename"/> を参照してください。
        ///         </description>
        ///     </item>
        /// </list>
        /// </remarks>
        public FigureNameType NameType
        {
            get
            {
                if (string.IsNullOrEmpty(Name)) return FigureNameType.BuiltIn;
                else if (Name[0] == '*') return FigureNameType.File;
                else return FigureNameType.Figure;
            }
        }

        /// <summary>
        /// <see cref="NameType"/> が <see cref="FigureNameType.File"/> の場合は画像ファイルのパス。
        /// それ以外の場合は <c>null</c>。
        /// </summary>
        /// <seealso cref="NameType"/>
        public string? Filename
        {
            get => NameType == FigureNameType.File ? Name.Substring(1) : null;
            set => Name = $"*{value}";
        }

        /// <summary>
        /// <see cref="PartialFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        public PartialFilterEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="PartialFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public PartialFilterEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="PartialFilterEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public PartialFilterEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            FigureType = (FigureType)data.Slice(0, 4).ToInt32();
            Name = data.Slice(4, MaxNameLength).ToCleanSjisString();
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            ((int)FigureType).ToBytes().CopyTo(data, 0);
            Name.ToSjisBytes(MaxNameLength).CopyTo(data, 4);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("type=");
            writer.WriteLine((int)FigureType);
            writer.Write("name=");
            writer.WriteLine(Name);
        }

        static PartialFilterEffect()
        {
            EffectType = new EffectType(
                88, 0x44000500, 6, 2, 260, "部分フィルタ",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("X", 10, -40000, 40000, 0),
                    new TrackbarDefinition("Y", 10, -40000, 40000, 0),
                    new TrackbarDefinition("回転", 100, -360000, 360000, 0),
                    new TrackbarDefinition("サイズ", 1, 0, 4000, 100),
                    new TrackbarDefinition("縦横比", 10, -1000, 1000, 0),
                    new TrackbarDefinition("ぼかし", 1, 0, 1000, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("背景", false, 0),
                    new CheckboxDefinition("マスクの反転", true, 0),
                }
            );
        }
    }
}
