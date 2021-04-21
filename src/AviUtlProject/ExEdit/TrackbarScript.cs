using System;
using Karoterra.AviUtlProject.Extensions;

namespace Karoterra.AviUtlProject.ExEdit
{
    public class TrackbarScript
    {
        public static readonly int Size = 128;

        public string Name { get; set; }

        public TrackbarScript(ReadOnlySpan<byte> data)
        {
            if (data.Length < Size)
            {
                throw new ArgumentException($"data length requires {Size} bytes.");
            }
            Name = data.ToSjisString().CutNull();
        }

        public void Dump(Span<byte> data)
        {
            Name.ToSjisBytes().CopyTo(data);
        }
    }
}
