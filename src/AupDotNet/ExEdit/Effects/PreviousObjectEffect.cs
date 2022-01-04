using System;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 直前オブジェクト
    /// </summary>
    public class PreviousObjectEffect : Effect
    {
        /// <summary>
        /// 直前オブジェクトのフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>
        /// <see cref="PreviousObjectEffect"/> のインスタンスを初期化します。
        /// </summary>
        public PreviousObjectEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="PreviousObjectEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public PreviousObjectEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static PreviousObjectEffect()
        {
            EffectType = new EffectType(
                9, 0x04000008, 0, 0, 0, "直前オブジェクト",
                new TrackbarDefinition[] {},
                new CheckboxDefinition[] {}
            );
        }
    }
}
