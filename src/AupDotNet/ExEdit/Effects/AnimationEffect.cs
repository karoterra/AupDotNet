using System;
using System.Collections.Generic;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// アニメーション効果
    /// </summary>
    public class AnimationEffect : ScriptFileEffect
    {
        public static EffectType EffectType { get; }

        /// <summary>
        /// exedit.anmで定義されているスクリプト。
        /// Nameが空の時にはこれのインデックスがScriptIdに入る。
        /// </summary>
        public static readonly IReadOnlyList<string> Defaults;

        public AnimationEffect()
            : base(EffectType)
        {
        }

        public AnimationEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public AnimationEffect(Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : base(EffectType, trackbars, checkboxes, data)
        {
        }

        static AnimationEffect()
        {
            EffectType = new EffectType(
                79, 0x04000420, 4, 2, 516, "アニメーション効果",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("track0", 100, 0, 0, 0),
                    new TrackbarDefinition("track1", 100, 0, 0, 0),
                    new TrackbarDefinition("track2", 100, 0, 0, 0),
                    new TrackbarDefinition("track3", 100, 0, 0, 0),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("震える", false, 0),
                    new CheckboxDefinition("check0", true, 0),
                }
            );
            Defaults = new List<string>()
            {
            "震える",
            "振り子",
            "弾む",
            "座標の拡大縮小(個別オブジェクト)",
            "画面外から登場",
            "ランダム方向から登場",
            "拡大縮小して登場",
            "ランダム間隔で落ちながら登場",
            "弾んで登場",
            "広がって登場",
            "起き上がって登場",
            "何処からともなく登場",
            "反復移動",
            "座標の回転(個別オブジェクト)",
            "立方体(カメラ制御)",
            "球体(カメラ制御)",
            "砕け散る",
            "点滅",
            "点滅して登場",
            "簡易変形",
            "簡易変形(カメラ制御)",
            "リール回転",
            "万華鏡",
            "円形配置",
            "ランダム配置",
            };
        }
    }
}
