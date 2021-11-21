using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// ルミナンスキー
    /// </summary>
    public class LuminanceKeyEffect : Effect
    {
        private const int Id = (int)EffectTypeId.LuminanceKey;

        /// <summary>基準輝度</summary>
        public Trackbar Reference => Trackbars[0];

        /// <summary>ぼかし</summary>
        public Trackbar Blur => Trackbars[1];

        /// <summary>
        /// 種類
        /// <list type="bullet">
        ///     <item>0. 暗い部分を透過</item>
        ///     <item>1. 明るい部分を透過</item>
        ///     <item>2. 明暗部分を透過</item>
        ///     <item>3. 明暗部分を透過(ぼかし無し)</item>
        /// </list>
        /// </summary>
        public int TransparentType { get; set; }

        public LuminanceKeyEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public LuminanceKeyEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public LuminanceKeyEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    TransparentType = span.ToInt32();
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
            TransparentType.ToBytes().CopyTo(data, 0);
            return data;
        }
    }
}
