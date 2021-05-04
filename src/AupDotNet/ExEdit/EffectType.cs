using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit
{
    public partial class EffectType
    {
        public static readonly int Size = 112;
        public static readonly int MaxNameLength = 96;

        public readonly int Id;

        public readonly uint Flag;
        public readonly uint TrackbarNum;
        public readonly uint CheckboxNum;
        public readonly uint ExtSize;
        public readonly string Name;

        public EffectType(int id, uint flag, uint trackbarNum, uint checkboxNum, uint extSize, string name)
        {
            Id = id;
            Flag = flag;
            TrackbarNum = trackbarNum;
            CheckboxNum = checkboxNum;
            ExtSize = extSize;
            Name = name;
        }

        public EffectType(ReadOnlySpan<byte> data, int id)
        {
            if (data.Length < Size)
            {
                throw new ArgumentException($"data length requires {Size} bytes.");
            }
            Id = id;
            Flag = data.Slice(0, 4).ToUInt32();
            TrackbarNum = data.Slice(4, 4).ToUInt32();
            CheckboxNum = data.Slice(8, 4).ToUInt32();
            ExtSize = data.Slice(12, 4).ToUInt32();
            Name = data.Slice(16).ToCleanSjisString();
        }

        public void Dump(Span<byte> data)
        {
            Flag.ToBytes().CopyTo(data);
            TrackbarNum.ToBytes().CopyTo(data.Slice(4));
            CheckboxNum.ToBytes().CopyTo(data.Slice(8));
            ExtSize.ToBytes().CopyTo(data.Slice(12));
            Name.ToSjisBytes(MaxNameLength).CopyTo(data.Slice(16, MaxNameLength));
        }
    }
}
