using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit
{
    public class Layer
    {
        public static readonly int Size = 76;
        public static readonly int MaxNameLength = 64;

        public uint SceneIndex { get; set; }
        public uint LayerIndex { get; set; }
        public LayerFlag Flag { get; set; }

        private string _name;
        public string Name {
            get => _name;
            set
            {
                if (value.GetSjisByteCount() >= MaxNameLength)
                {
                    throw new ArgumentException($"Byte count of {nameof(Name)} must be less than {MaxNameLength}.");
                }
                _name = value;
            }
        }

        public Layer(ReadOnlySpan<byte> data)
        {
            if (data.Length < Size)
            {
                throw new ArgumentException($"data length requires {Size} bytes.");
            }
            SceneIndex = data.Slice(0, 4).ToUInt32();
            LayerIndex = data.Slice(4, 4).ToUInt32();
            Flag = (LayerFlag)data.Slice(8, 4).ToUInt32();
            Name = data.Slice(12).ToCleanSjisString();
        }

        public void Dump(Span<byte> data)
        {
            SceneIndex.ToBytes().CopyTo(data);
            LayerIndex.ToBytes().CopyTo(data.Slice(4));
            ((uint)Flag).ToBytes().CopyTo(data.Slice(8));
            Name.ToSjisBytes(MaxNameLength).CopyTo(data.Slice(12, MaxNameLength));
        }
    }
}
