using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>アルファチャンネルを読み込む</summary>
        public bool Alpha
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

        public byte[] Data { get; } = new byte[24];

        public VideoFileEffect()
            : base(EffectType)
        {
        }

        public VideoFileEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public VideoFileEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Filename = data.Slice(0, MaxFilenameLength).ToCleanSjisString();
            data.Slice(MaxFilenameLength, 24).CopyTo(Data);
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
