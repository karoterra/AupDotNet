using System;
using System.Collections.Generic;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit
{
    public class TrackbarScript
    {
        public static readonly int Size = 128;

        /// <summary>
        /// exedit.traで定義されているスクリプト。
        /// </summary>
        public static readonly IReadOnlyList<string> Defaults;

        private string _name;
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

        static TrackbarScript()
        {
            Defaults = new List<string>()
            {
                "補間移動",
                "回転",
            };
        }
    }
}
