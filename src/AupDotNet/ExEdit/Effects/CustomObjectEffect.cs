using System;
using System.Collections.Generic;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// カスタムオブジェクト
    /// </summary>
    public class CustomObjectEffect : ScriptFileEffect
    {
        public static EffectType EffectType { get; }

        /// <summary>
        /// exedit.objで定義されているスクリプト。
        /// Nameが空の時にはこれのインデックスがScriptIdに入る。
        /// </summary>
        public static readonly IReadOnlyList<string> Defaults;

        public CustomObjectEffect()
            : base(EffectType)
        {
        }

        public CustomObjectEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public CustomObjectEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        static CustomObjectEffect()
        {
            EffectType = new EffectType(
                80, 0x04000408, 4, 2, 516, "カスタムオブジェクト",
                new TrackbarDefinition[]
                {

                    new TrackbarDefinition("track0", 100, 0, 0, 0),
                    new TrackbarDefinition("track1", 100, 0, 0, 0),
                    new TrackbarDefinition("track2", 100, 0, 0, 0),
                    new TrackbarDefinition("track3", 100, 0, 0, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("集中線", false, 0),
                    new CheckboxDefinition("check0", true, 0),
                }
            );
            Defaults = new List<string>()
            {
                "集中線",
                "走査線",
                "カウンター",
                "レンズフレア",
                "雲",
                "星",
                "雪",
                "雨",
                "ランダム小物配置(カメラ制御)",
                "ライン(移動軌跡)",
                "扇型",
                "多角形",
                "周辺ボケ光量",
                "フレア",
                "水面",
            };
        }
    }
}
