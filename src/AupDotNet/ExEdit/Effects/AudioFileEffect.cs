using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 音声ファイル
    /// </summary>
    public class AudioFileEffect : Effect
    {
        public readonly int MaxFilenameLength = 260;
        public static EffectType EffectType { get; }

        /// <summary>再生位置</summary>
        public Trackbar Position => Trackbars[0];

        /// <summary>再生速度</summary>
        public Trackbar Speed => Trackbars[1];

        /// <summary>ループ再生</summary>
        public bool Loop
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>動画ファイルと連携</summary>
        public bool LinkVideo
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        private string _filename = "";
        /// <summary>参照ファイル</summary>
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
            : base(EffectType)
        {
        }

        public AudioFileEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public AudioFileEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Filename = data.Slice(0, MaxFilenameLength).ToCleanSjisString();
            data.Slice(MaxFilenameLength, 20).CopyTo(Data);
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Filename.ToSjisBytes(MaxFilenameLength).CopyTo(data, 0);
            Data.CopyTo(data, MaxFilenameLength);
            return data;
        }

        public override void ExportExtData(TextWriter writer)
        {
            writer.Write("file=");
            writer.WriteLine(Filename);
        }

        static AudioFileEffect()
        {
            EffectType = new EffectType(
                2, 0x04200408, 2, 3, 280, "音声ファイル",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("再生位置", 100, 0, 0, 0),
                    new TrackbarDefinition("再生速度", 10, 100, 8000, 1000),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("ループ再生", true, 0),
                    new CheckboxDefinition("動画ファイルと連携", true, 1),
                    new CheckboxDefinition("参照ファイル", false, 0),
                }
            );
        }
    }
}
