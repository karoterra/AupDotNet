using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public class ImageCompositionEffect : Effect
    {
        public readonly int MaxFilenameLength = 256;
        private const int Id = (int)EffectTypeId.ImageComposition;

        public Trackbar X => Trackbars[0];
        public Trackbar Y => Trackbars[1];
        public Trackbar Zoom => Trackbars[2];

        /// <summary>ループ画像</summary>
        public bool Loop
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
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

        /// <summary>
        /// <list type="bullet">
        ///     <item>0. 前方から合成</item>
        ///     <item>1. 後方から合成</item>
        ///     <item>2. 色情報を上書き</item>
        ///     <item>3. 輝度をアルファ値として上書き</item>
        ///     <item>4. 輝度をアルファ値として乗算</item>
        /// </list>
        /// </summary>
        public int Mode { get; set; }

        public ImageCompositionEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public ImageCompositionEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public ImageCompositionEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    Mode = span.Slice(0, 4).ToInt32();
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
            Mode.ToBytes().CopyTo(data, 0);
            Filename.ToSjisBytes(MaxFilenameLength).CopyTo(data, 4);
            return data;
        }
    }
}
