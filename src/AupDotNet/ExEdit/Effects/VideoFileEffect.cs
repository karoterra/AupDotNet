using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 動画ファイル
    /// </summary>
    public class VideoFileEffect : Effect
    {
        public readonly int MaxFilenameLength = 260;
        public static EffectType EffectType { get; }

        public Trackbar Position => Trackbars[0];
        public Trackbar Speed => Trackbars[1];

        public bool Loop
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public bool Alpha
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

        public byte[] Data { get; } = new byte[24];

        public VideoFileEffect()
            : base(EffectType)
        {
        }

        public VideoFileEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public VideoFileEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType, trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    Filename = span.Slice(0, MaxFilenameLength).ToCleanSjisString();
                    Data = span.Slice(MaxFilenameLength, 24).ToArray();
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

        static VideoFileEffect()
        {
            EffectType = new EffectType(
                0, 0x04000448, 2, 3, 284, "動画ファイル",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("再生位置", 1, 0, 0, 1),
                    new TrackbarDefinition("再生速度", 10, -20000, 20000, 1000),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("ループ再生", true, 0),
                    new CheckboxDefinition("アルファチャンネルを読み込む", true, 0),
                    new CheckboxDefinition("参照ファイル", false, 0),
                }
            );
        }
    }
}
