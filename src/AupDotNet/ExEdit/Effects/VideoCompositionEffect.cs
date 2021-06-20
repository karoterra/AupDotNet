using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public class VideoCompositionEffect : Effect
    {
        public readonly int MaxFilenameLength = 260;
        private const int Id = (int)EffectTypeId.VideoComposition;

        public Trackbar Position => Trackbars[0];
        public Trackbar Speed => Trackbars[1];
        public Trackbar X => Trackbars[2];
        public Trackbar Y => Trackbars[3];
        public Trackbar Zoom => Trackbars[4];

        public bool LoopPlayback
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public bool Sync
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        public bool LoopImage
        {
            get => Checkboxes[2] != 0;
            set => Checkboxes[2] = value ? 1 : 0;
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

        public byte[] Field0x104 { get; set; } = new byte[20];

        public int Mode { get; set; }

        public VideoCompositionEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public VideoCompositionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public VideoCompositionEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    Filename = span.Slice(0, MaxFilenameLength).ToCleanSjisString();
                    Field0x104 = span.Slice(0x104, 20).ToArray();
                    Mode = span.Slice(0x118, 4).ToInt32();
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
            Field0x104.CopyTo(data, 0x104);
            Mode.ToBytes().CopyTo(data, 0x118);
            return data;
        }
    }
}
