using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public class MaskEffect : Effect
    {
        public readonly int MaxNameLength = 256;
        public static EffectType EffectType { get; }

        public Trackbar X => Trackbars[0];
        public Trackbar Y => Trackbars[1];
        public Trackbar Rotate => Trackbars[2];
        public Trackbar Size => Trackbars[3];
        public Trackbar AspectRatio => Trackbars[4];
        public Trackbar Blur => Trackbars[5];

        /// <summary>マスクの反転</summary>
        public bool Invert
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

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
        /// 組込み図形以外のマスクを使用している場合は <see cref="Name"/> にその情報が格納されます。
        /// </remarks>
        /// <seealso cref="NameType"/>
        public FigureType FigureType { get; set; } = FigureType.Square;

        public string _name = "";
        /// <summary>
        /// マスクの名前
        /// </summary>
        /// <remarks>
        /// 組込み図形以外のマスクを使用している場合はここにその情報が格納されます。
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
        public string Filename
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
                bool success = int.TryParse(Name.Substring(1), out int scene);
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

        public MaskEffect()
            : base(EffectType)
        {
        }

        public MaskEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public MaskEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            FigureType = (FigureType)data.Slice(0, 4).ToInt32();
            Name = data.Slice(4, MaxNameLength).ToCleanSjisString();
            Mode = data.Slice(0x104, 4).ToInt32();
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            ((int)FigureType).ToBytes().CopyTo(data, 0);
            Name.ToSjisBytes(MaxNameLength).CopyTo(data, 4);
            Mode.ToBytes().CopyTo(data, 0x104);
            return data;
        }

        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("type=");
            writer.WriteLine((int)FigureType);
            writer.Write("name=");
            writer.WriteLine(Name);
            writer.Write("mode=");
            writer.WriteLine(Mode);
        }

        static MaskEffect()
        {
            EffectType = new EffectType(
                41, 0x04000620, 6, 3, 264, "マスク",
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
                    new CheckboxDefinition("元のサイズに合わせる", true, 0),
                }
            );
        }
    }
}
