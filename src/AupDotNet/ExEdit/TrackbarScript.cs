using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit
{
    public class TrackbarScript
    {
        public static readonly int Size = 128;

        /// <summary>
        /// exedit.traで定義されているスクリプト。
        /// </summary>
        public static readonly IReadOnlyList<TrackbarScript> Defaults;

        /// <summary>
        /// 拡張編集組込みのトラックバー変化方法
        /// </summary>
        public static readonly IReadOnlyList<TrackbarScript> BuiltIn;

        private string _name = "";
        public string Name
        {
            get => _name;
            set
            {
                if (value.GetSjisByteCount() >= Size)
                {
                    throw new MaxByteCountOfStringException(nameof(Name), Size);
                }
                _name = value;
            }
        }

        public string Filename
        {
            get
            {
                if (Defaults.Select(x => x.Name).Contains(Name))
                    return "exedit.tra";

                var s = Name.Split(delim, 2);
                if (s.Length == 2)
                    return $"@{s[1]}.tra";
                return $"{Name}.tra";
            }
        }

        public bool EnableTwoPoint { get; set; } = false;
        public bool EnableParam { get; set; } = true;
        public bool EnableSpeed { get; set; } = true;

        private static readonly char[] delim = new[] { '@' };
        private static readonly string twopointSign = "--twopoint";
        private static readonly string paramSign = "--param:";
        private static readonly string speedSign = "--speed:";

        public TrackbarScript()
        {
        }

        public TrackbarScript(string name)
        {
            Name = name;
        }

        public TrackbarScript(ReadOnlySpan<byte> data)
        {
            if (data.Length < Size)
            {
                throw new ArgumentException($"data length requires {Size} bytes.");
            }
            Name = data.ToCleanSjisString();
        }

        public void Dump(Span<byte> data)
        {
            Name.ToSjisBytes(Size).CopyTo(data);
        }

        public void ParseScript(string script)
        {
            EnableTwoPoint = false;
            EnableParam = false;
            EnableSpeed = false;

            using (var reader = new StringReader(script))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith(twopointSign)) EnableTwoPoint = true;
                    else if (line.StartsWith(paramSign)) EnableParam = true;
                    else if (line.StartsWith(speedSign)) EnableSpeed = true;
                }
            }
        }

        static TrackbarScript()
        {
            Defaults = new List<TrackbarScript>()
            {
                new TrackbarScript("補間移動") { EnableParam = false, EnableSpeed = true, EnableTwoPoint = false },
                new TrackbarScript("回転") { EnableParam = true, EnableSpeed = false, EnableTwoPoint = true },
            };

            BuiltIn = new List<TrackbarScript>()
            {
                new TrackbarScript("移動無し") { EnableParam = false, EnableSpeed = false, EnableTwoPoint = false },
                new TrackbarScript("直線移動") { EnableParam = false, EnableSpeed = true, EnableTwoPoint = false },
                new TrackbarScript("曲線移動") { EnableParam = false, EnableSpeed = true, EnableTwoPoint = false },
                new TrackbarScript("瞬間移動") { EnableParam = false, EnableSpeed = false, EnableTwoPoint = false },
                new TrackbarScript("中間点無視") { EnableParam = false, EnableSpeed = true, EnableTwoPoint = true },
                new TrackbarScript("移動量指定") { EnableParam = false, EnableSpeed = false, EnableTwoPoint = false },
                new TrackbarScript("ランダム移動") { EnableParam = true, EnableSpeed = false, EnableTwoPoint = false },
                new TrackbarScript("加減速移動") { EnableParam = false, EnableSpeed = true, EnableTwoPoint = false },
                new TrackbarScript("反復移動") { EnableParam = true, EnableSpeed = true, EnableTwoPoint = false },
            };
        }
    }
}
