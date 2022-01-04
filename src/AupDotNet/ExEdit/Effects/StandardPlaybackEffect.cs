using System;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 標準再生
    /// </summary>
    public class StandardPlaybackEffect : Effect
    {
        /// <summary>
        /// 標準再生のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>音量</summary>
        public Trackbar Volume => Trackbars[0];

        /// <summary>左右</summary>
        public Trackbar Pan => Trackbars[1];

        /// <summary>
        /// <see cref="StandardPlaybackEffect"/> のインスタンスを初期化します。
        /// </summary>
        public StandardPlaybackEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="StandardPlaybackEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public StandardPlaybackEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        static StandardPlaybackEffect()
        {
            EffectType = new EffectType(
                12, 0x04200090, 2, 0, 0, "標準再生",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("音量", 10, 0, 5000, 1000),
                    new TrackbarDefinition("左右", 10, -1000, 1000, 0),
                },
                new CheckboxDefinition[] { }
            );
        }
    }
}
