using System;
using System.Text;
using System.Collections.Generic;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public abstract class ScriptFileEffect : Effect
    {
        public readonly int MaxNameLength = 256;
        public readonly int MaxParamsLength = 256;

        public bool Check0
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        public int ScriptId { get; set; }
        public ScriptDirectory Directory { get; set; }

        private string _name = "";
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

        public Dictionary<string, string> Params { get; set; }

        public ScriptFileEffect(EffectType type)
            : base(type)
        {
        }

        public ScriptFileEffect(EffectType type, Trackbar[] trackbars, int[] checkboxes)
            : base(type, trackbars, checkboxes)
        {
        }

        public ScriptFileEffect(EffectType type, Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(type, trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    ScriptId = span.Slice(0, 2).ToInt16();
                    Directory = (ScriptDirectory)span.Slice(2, 2).ToInt16();
                    Name = span.Slice(4, MaxNameLength).ToCleanSjisString();
                    Params = ParseParams(span.Slice(0x104, MaxParamsLength).ToCleanSjisString());
                }
                else if (data.Length != 0)
                {
                    throw new ArgumentException("data's length is invalid.");
                }
            }
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            ((short)ScriptId).ToBytes().CopyTo(data, 0);
            ((short)Directory).ToBytes().CopyTo(data, 2);
            Name.ToSjisBytes(MaxNameLength).CopyTo(data, 4);
            BuildParams().ToSjisBytes(MaxParamsLength).CopyTo(data, 0x104);
            return data;
        }

        public static Dictionary<string, string> ParseParams(string str)
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
                    if (str[i] == '"' && str[i-1] != '\\')
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

            StringBuilder sb = new StringBuilder();
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
