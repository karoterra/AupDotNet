using System;
using System.Collections.Generic;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// カメラ効果
    /// </summary>
    public class CameraEffect : ScriptFileEffect
    {
        /// <summary>
        /// カメラ効果のフィルタ効果定義。
        /// </summary>
        public static EffectType EffectType { get; }

        /// <summary>
        /// exedit.camで定義されているスクリプト。
        /// Nameが空の時にはこれのインデックスがScriptIdに入る。
        /// </summary>
        public static readonly IReadOnlyList<string> Defaults;

        /// <summary>
        /// <see cref="CameraEffect"/> のインスタンスを初期化します。
        /// </summary>
        public CameraEffect()
            : base(EffectType)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックスの値を指定して <see cref="CameraEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        public CameraEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        /// <summary>
        /// トラックバーとチェックボックス、拡張データを指定して <see cref="CameraEffect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        public CameraEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        static CameraEffect()
        {
            EffectType = new EffectType(
                97, 0x05000400, 4, 2, 516, "カメラ効果",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("track0", 100, 0, 0, 0),
                    new TrackbarDefinition("track1", 100, 0, 0, 0),
                    new TrackbarDefinition("track2", 100, 0, 0, 0),
                    new TrackbarDefinition("track3", 100, 0, 0, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("手ぶれ", false, 0),
                    new CheckboxDefinition("check0", true, 0),
                }
            );
            Defaults = new string[]
            {
                "手ぶれ",
                "目標中心回転",
                "目標サイズ固定視野角",
            };
        }
    }
}
