using System;
using System.Text;
using System.Collections.Generic;
using Karoterra.AupDotNet.Extensions;
using System.IO;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// <see cref="AnimationEffect"/>, <see cref="CustomObjectEffect"/>, <see cref="CameraEffect"/>, <see cref="SceneChangeEffect"/> 用のベースクラス。
    /// </summary>
    public abstract class ScriptFileEffect : Effect
    {
        /// <summary>
        /// <see cref="Name"/> の最大バイト数。
        /// </summary>
        public static readonly int MaxNameLength = 256;

        /// <summary>
        /// <see cref="Params"/> を文字列にしたときの最大バイト数。
        /// </summary>
        public static readonly int MaxParamsLength = 256;

        /// <summary>
        /// <c>obj.check0</c>
        /// </summary>
        public bool Check0
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>
        /// スクリプト番号。
        /// </summary>
        public short ScriptId { get; set; }

        /// <summary>
        /// スクリプトのディレクトリフィルタ。
        /// </summary>
        public ScriptDirectory Directory { get; set; }

        private string _name = "";
        /// <summary>
        /// 名前。
        /// </summary>
        /// <remarks>
        /// 文字列の最大バイト数は <see cref="MaxNameLength"/> です。
        /// </remarks>
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
        /// スクリプトのパラメータ。
        /// キーは変数名、値は変数の値です。
        /// </summary>
        /// <remarks>
        /// 拡張編集のスクリプトファイルでは <c>--param</c>, <c>--color</c>, <c>--file</c>, <c>--dialog</c> によってパラメータを保存することができます。
        /// これは拡張データに <c>変数名1=値1;変数名2=値2;</c> のような形式で保存されます。
        /// <c>Params</c> はこの変数名を Key、値を Value とする<see cref="Dictionary{TKey, TValue}"/>です。
        /// 変数が <c>local</c> の場合は Key は <c>local 変数名</c> になります。
        /// 拡張データに保存するときの最大バイト数は <see cref="MaxParamsLength"/> です。
        /// </remarks>
        /// <seealso cref="ParseParams(string)"/>
        /// <seealso cref="BuildParams"/>
        public Dictionary<string, string>? Params { get; set; }

        /// <summary>
        /// フィルタ効果定義を指定して <see cref="ScriptFileEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="type">フィルタ効果定義</param>
        protected ScriptFileEffect(EffectType type)
            : base(type)
        {
        }

        /// <summary>
        /// フィルタ効果定義とトラックバー、チェックボックスの値を指定して <see cref="ScriptFileEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="type">フィルタ効果定義</param>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        protected ScriptFileEffect(EffectType type, Trackbar[] trackbars, int[] checkboxes)
            : base(type, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// フィルタ効果定義とトラックバー、チェックボックス、拡張データを指定して <see cref="ScriptFileEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="type">フィルタ効果定義</param>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        protected ScriptFileEffect(EffectType type, Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(type, trackbars, checkboxes, data)
        {
        }

        /// <inheritdoc/>
        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            ScriptId = data.Slice(0, 2).ToInt16();
            Directory = (ScriptDirectory)data.Slice(2, 2).ToInt16();
            Name = data.Slice(4, MaxNameLength).ToCleanSjisString();
            Params = ParseParams(data.Slice(0x104, MaxParamsLength).ToCleanSjisString());
        }

        /// <inheritdoc/>
        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            ScriptId.ToBytes().CopyTo(data, 0);
            ((short)Directory).ToBytes().CopyTo(data, 2);
            Name.ToSjisBytes(MaxNameLength).CopyTo(data, 4);
            BuildParams().ToSjisBytes(MaxParamsLength).CopyTo(data, 0x104);
            return data;
        }

        /// <inheritdoc/>
        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("type=");
            writer.WriteLine(ScriptId);
            writer.Write("filter=");
            writer.WriteLine((int)Directory);
            writer.Write("name=");
            writer.WriteLine(Name);
            writer.Write("param=");
            writer.WriteLine(BuildParams());
        }

        /// <summary>
        /// スクリプトファイルのパラメータ文字列を <see cref="Params"/> が扱う辞書形式に構成します。
        /// </summary>
        /// <param name="str">スクリプトファイルのパラメータ文字列</param>
        /// <returns><see cref="Params"/> が扱う辞書</returns>
        public static Dictionary<string, string>? ParseParams(string str)
        {
            var dic = new Dictionary<string, string>();
            if (str == null || str == "")
            {
                return dic;
            }
            else if (str[0] == '*')
            {
                return null;
            }

            int i = 0;
            while (i < str.Length)
            {
                int start = i;
                for (; i < str.Length; i++)
                {
                    if (str[i] == '=')
                    {
                        break;
                    }
                }
                var key = str.Substring(start, i - start);

                start = ++i;
                bool isString = false;
                for (; i < str.Length; i++)
                {
                    if (str[i] == '"' && str[i - 1] != '\\')
                    {
                        isString = !isString;
                    }
                    if (!isString && str[i] == ';')
                    {
                        break;
                    }
                }
                var val = str.Substring(start, i - start);

                dic[key] = val;
                i++;
            }

            return dic;
        }

        /// <summary>
        /// <see cref="Params"/> を拡張データに保存可能な文字列形式に変換します。
        /// </summary>
        /// <returns>パラメータ文字列</returns>
        public string BuildParams()
        {
            if (Params == null)
            {
                return "*";
            }
            if (Params.Keys.Count == 1)
            {
                if (Params.ContainsKey("file"))
                {
                    return $"file={Params["file"]}";
                }
                if (Params.ContainsKey("color"))
                {
                    return $"color={Params["color"]}";
                }
            }

            StringBuilder sb = new();
            foreach (var pair in Params)
            {
                sb.Append(pair.Key);
                sb.Append('=');
                sb.Append(pair.Value);
                sb.Append(';');
            }
            return sb.ToString();
        }
    }
}
