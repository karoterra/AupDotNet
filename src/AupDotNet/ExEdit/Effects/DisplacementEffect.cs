using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ディスプレイスメントマップの変形方法
    /// </summary>
    public enum DisplacementCalc
    {
        /// <summary>移動変形</summary>
        Move = 0,
        /// <summary>拡大変形</summary>
        Scale,
        /// <summary>回転変形</summary>
        Rotate,
    }

    /// <summary>
    /// ディスプレイスメントマップ
    /// </summary>
    public class DisplacementEffect : Effect
    {
        /// <summary>
        /// マップの名前の最大バイト数。
        /// </summary>
        public static readonly int MaxNameLength = 256;

        /// <summary>
        /// ディスプレイスメントマップのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>変形X,拡大変形,回転変形</summary>
        public Trackbar Param0 => Trackbars[0];

        /// <summary>変形Y,拡大縦横,----</summary>
        public Trackbar Param1 => Trackbars[1];

        /// <summary>X</summary>
        public Trackbar X => Trackbars[2];

        /// <summary>Y</summary>
        public Trackbar Y => Trackbars[3];

        /// <summary>回転</summary>
        public Trackbar Rotate => Trackbars[4];

        /// <summary>サイズ</summary>
        public Trackbar Size => Trackbars[5];

        /// <summary>縦横比</summary>
        public Trackbar AspectRatio => Trackbars[6];

        /// <summary>ぼかし</summary>
        public Trackbar Blur => Trackbars[7];

        /// <summary>元のサイズに合わせる</summary>
        public bool Fit
        {
            get => Checkboxes[2] != 0;
            set => Checkboxes[2] = value ? 1 : 0;
        }

        /// <summary>
        /// 使用している組込み図形の種類
        /// </summary>
        /// <remarks>
        /// 組込み図形以外のマップを使用している場合は <see cref="Name"/> にその情報が格納されます。
        /// </remarks>
        /// <seealso cref="NameType"/>
        public FigureType FigureType { get; set; } = FigureType.Circle;

        private string _name = "";
        /// <summary>
        /// マップの名前
        /// </summary>
        /// <remarks>
        /// 組込み図形以外のマップを使用している場合はここにその情報が格納されます。
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
        /// マップの種類
        /// </summary>
        /// <remarks>
        /// 組込み図形以外のマップを使用している場合は <see cref="Name"/> にその情報が格納されます。
        /// <c>NameType</c> は <c>Name</c> がどのような種類のマップの情報を格納しているかを表します。
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
        ///     <item>
        ///         <term><see cref="FigureNameType.Scene"/></term>
        ///         <description>
        ///             シーンが参照されています。
        ///             <see cref="Name"/> にはシーン番号の前に <c>:</c> が付いたものが入っています。
        ///             シーン番号を取得する場合は <see cref="Filename"/> を参照してください。
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
                else if (Name[0] == ':') return FigureNameType.Scene;
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
        /// <see cref="NameType"/> が <see cref="FigureNameType.Scene"/> の場合はシーン番号。
        /// それ以外の場合は <c>null</c>。
        /// </summary>
        /// <seealso cref="NameType"/>
        public int? Scene
        {
            get
            {
                if (NameType != FigureNameType.Scene) return null;
#if NETSTANDARD2_0
                bool success = int.TryParse(Name.Substring(1), out int scene);
#else
                bool success = int.TryParse(Name.AsSpan(1), out int scene);
#endif
                if (success) return scene;
                else return null;
            }
            set
            {
                if (!value.HasValue) value = 0;
                Name = $":{value}";
            }
        }

        /// <summary>
        /// シーン選択時に有効。
        /// 1: シーンの長さを合わせる
        /// 2: シーンを逆再生
        /// </summary>
        public int Mode { get; set; }

        /// <summary>変形方法</summary>
        public DisplacementCalc Calc { get; set; }

        /// <summary>
        /// <see cref="DisplacementEffect"/> のインスタンスを初期化します。
        /// </summary>
        public DisplacementEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="DisplacementEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public DisplacementEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="DisplacementEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public DisplacementEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            FigureType = (FigureType)data.Slice(0, 4).ToInt32();
            Name = data.Slice(4, MaxNameLength).ToCleanSjisString();
            Mode = data.Slice(0x104, 4).ToInt32();
            Calc = (DisplacementCalc)data.Slice(0x108, 4).ToInt32();
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            ((int)FigureType).ToBytes().CopyTo(data, 0);
            Name.ToSjisBytes(MaxNameLength).CopyTo(data, 4);
            Mode.ToBytes().CopyTo(data, 0x104);
            ((int)Calc).ToBytes().CopyTo(data, 0x108);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("type=");
            writer.WriteLine((int)FigureType);
            writer.Write("name=");
            writer.WriteLine(Name);
            writer.Write("mode=");
            writer.WriteLine(Mode);
            writer.Write("calc=");
            writer.WriteLine((int)Calc);
        }

        static DisplacementEffect()
        {
            EffectType = new EffectType(
                69, 0x04000620, 8, 3, 268, "ディスプレイスメントマップ",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("param0", 10, -40000, 40000, 0),
                    new TrackbarDefinition("param1", 10, -40000, 40000, 0),
                    new TrackbarDefinition("X", 10, -40000, 40000, 0),
                    new TrackbarDefinition("Y", 10, -40000, 40000, 0),
                    new TrackbarDefinition("回転", 100, -360000, 360000, 0),
                    new TrackbarDefinition("サイズ", 1, 0, 4000, 200),
                    new TrackbarDefinition("縦横比", 10, -1000, 1000, 0),
                    new TrackbarDefinition("ぼかし", 1, 0, 1000, 5),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("背景", false, 0),
                    new CheckboxDefinition("移動変形", false, 0),
                    new CheckboxDefinition("元のサイズに合わせる", true, 0),
                }
            );
        }
    }
}
