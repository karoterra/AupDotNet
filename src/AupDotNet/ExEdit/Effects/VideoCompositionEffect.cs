using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public class VideoCompositionEffect : Effect
    {
        public readonly int MaxFilenameLength = 260;
        private const int Id = (int)EffectTypeId.VideoComposition;

        /// <summary>再生範囲,オフセット</summary>
        public Trackbar Position => Trackbars[0];

        /// <summary>再生速度</summary>
        public Trackbar Speed => Trackbars[1];

        /// <summary>X</summary>
        public Trackbar X => Trackbars[2];

        /// <summary>Y</summary>
        public Trackbar Y => Trackbars[3];

        /// <summary>拡大率</summary>
        public Trackbar Zoom => Trackbars[4];

        /// <summary>ループ再生</summary>
        public bool LoopPlayback
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>動画ファイルの同期</summary>
        public bool Sync
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>ループ画像</summary>
        public bool LoopImage
        {
            get => Checkboxes[2] != 0;
            set => Checkboxes[2] = value ? 1 : 0;
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

        public byte[] Field0x104 { get; set; } = new byte[20];

        /// <summary>
        /// <list type="bullet">
        ///     <item>0. 色情報を上書き</item>
        ///     <item>1. 輝度をアルファ値として上書き</item>
        ///     <item>2. 輝度をアルファ値として乗算</item>
        /// </list>
        /// </summary>
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
