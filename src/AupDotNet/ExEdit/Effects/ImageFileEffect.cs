using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public class ImageFileEffect : Effect
    {
        public readonly int MaxFilenameLength = 256;
        private const int Id = (int)EffectTypeId.ImageFile;

        public int Field0x0 { get; set; }

        private string _filename = "";
        public string Filename
        {
            get => _filename;
            set
            {
                if (value.GetSjisByteCount() >= MaxFilenameLength)
                {
                    throw new MaxByteCountOfStringException(nameof(Filename), MaxFilenameLength);
                }
                _filename = value;
            }
        }

        public ImageFileEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public ImageFileEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public ImageFileEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    Field0x0 = span.Slice(0, 4).ToInt32();
                    Filename = span.Slice(4, MaxFilenameLength).ToCleanSjisString();
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
            Field0x0.ToBytes().CopyTo(data, 0);
            Filename.ToSjisBytes(MaxFilenameLength).CopyTo(data, 4);
            return data;
        }
    }
}
