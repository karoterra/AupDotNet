using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public class AudioFileEffect : Effect
    {
        public readonly int MaxFilenameLength = 260;
        private const int Id = (int)EffectTypeId.AudioFile;

        public Trackbar Position => Trackbars[0];
        public Trackbar Speed => Trackbars[1];

        public bool Loop
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public bool LinkVideo
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

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

        public byte[] Data { get; } = new byte[20];

        public AudioFileEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public AudioFileEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public AudioFileEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    Filename = span.Slice(0, MaxFilenameLength).ToCleanSjisString();
                    Data = span.Slice(MaxFilenameLength, 20).ToArray();
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
            Filename.ToSjisBytes(MaxFilenameLength).CopyTo(data, 0);
            Data.CopyTo(data, MaxFilenameLength);
            return data;
        }
    }
}
