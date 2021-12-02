using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 画像ファイル
    /// </summary>
    public class ImageFileEffect : Effect
    {
        public readonly int MaxFilenameLength = 256;
        public static EffectType EffectType { get; }

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
            : base(EffectType)
        {
        }

        public ImageFileEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public ImageFileEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        protected override void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
            Field0x0 = data.Slice(0, 4).ToInt32();
            Filename = data.Slice(4, MaxFilenameLength).ToCleanSjisString();
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Field0x0.ToBytes().CopyTo(data, 0);
            Filename.ToSjisBytes(MaxFilenameLength).CopyTo(data, 4);
            return data;
        }

        static ImageFileEffect()
        {
            EffectType = new EffectType(
                1, 0x04000408, 0, 1, 260, "画像ファイル",
                new TrackbarDefinition[] {},
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("参照ファイル", false, 0),
                }
            );
        }
    }
}
