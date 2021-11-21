using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ワイプ
    /// </summary>
    public class WipeEffect : Effect
    {
        public readonly int MaxFilenameLength = 256;
        private const int Id = (int)EffectTypeId.Wipe;

        /// <summary>イン</summary>
        public Trackbar In => Trackbars[0];

        /// <summary>アウト</summary>
        public Trackbar Out => Trackbars[1];

        /// <summary>ぼかし</summary>
        public Trackbar Blur => Trackbars[2];

        /// <summary>反転(イン)</summary>
        public bool ReverseIn
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>反転(アウト)</summary>
        public bool ReverseOut
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>
        /// ワイプの種類
        /// <list type="bullet">
        ///     <item>0. ワイプ(円)</item>
        ///     <item>1. ワイプ(四角)</item>
        ///     <item>2. ワイプ(時計)</item>
        ///     <item>3. ワイプ(横)</item>
        ///     <item>4. ワイプ(縦)</item>
        /// </list>
        /// </summary>
        public int WipeType { get; set; }

        private string _filename = "";
        /// <summary>
        /// transitionフォルダの画像を選んだ時はファイル名(拡張子無し)がここに入る
        /// </summary>
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

        public WipeEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public WipeEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public WipeEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    WipeType = span.Slice(0, 4).ToInt32();
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
            WipeType.ToBytes().CopyTo(data, 0);
            Filename.ToSjisBytes(MaxFilenameLength).CopyTo(data, 4);
            return data;
        }
    }
}
