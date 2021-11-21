using System;
using System.Drawing;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// シャドー
    /// </summary>
    public class ShadowEffect : Effect
    {
        public readonly int MaxFilenameLength = 256;
        private const int Id = (int)EffectTypeId.Shadow;

        public Trackbar X => Trackbars[0];
        public Trackbar Y => Trackbars[1];

        /// <summary>濃さ</summary>
        public Trackbar Depth => Trackbars[2];

        /// <summary>拡散</summary>
        public Trackbar Diffusion => Trackbars[3];

        /// <summary>影を別オブジェクトで描画</summary>
        public bool Split
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>影色の設定</summary>
        public Color Color { get; set; } = Color.Black;

        private string _filename = "";
        /// <summary>パターン画像ファイル</summary>
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

        public ShadowEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public ShadowEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public ShadowEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    Color = span.Slice(0, 4).ToColor(true);
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
            Color.ToBytes(true).CopyTo(data, 0);
            Filename.ToSjisBytes(MaxFilenameLength).CopyTo(data, 4);
            return data;
        }
    }
}
