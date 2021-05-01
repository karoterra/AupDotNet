using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit
{
    public class FilterType
    {
        public static readonly int Size = 112;

        public readonly uint Flag;
        public readonly uint TrackbarNum;
        public readonly uint CheckboxNum;
        public readonly uint ExtSize;
        public readonly string Name;

        public FilterType(ReadOnlySpan<byte> data)
        {
            if (data.Length < Size)
            {
                throw new ArgumentException($"data length requires {Size} bytes.");
            }
            Flag = data.Slice(0, 4).ToUInt32();
            TrackbarNum = data.Slice(4, 4).ToUInt32();
            CheckboxNum = data.Slice(8, 4).ToUInt32();
            ExtSize = data.Slice(12, 4).ToUInt32();
            Name = data.Slice(16).ToSjisString().CutNull();
        }

        public void Dump(Span<byte> data)
        {
            Flag.ToBytes().CopyTo(data);
            TrackbarNum.ToBytes().CopyTo(data.Slice(4));
            CheckboxNum.ToBytes().CopyTo(data.Slice(8));
            ExtSize.ToBytes().CopyTo(data.Slice(12));
            Name.ToSjisBytes().CopyTo(data.Slice(16));
        }
    }
}
