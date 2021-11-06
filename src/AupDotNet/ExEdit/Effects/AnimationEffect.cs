using System.Collections.Generic;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// アニメーション効果
    /// </summary>
    public class AnimationEffect : ScriptFileEffect
    {
        private const int Id = (int)EffectTypeId.AnimationEffect;

        /// <summary>
        /// exedit.anmで定義されているスクリプト。
        /// Nameが空の時にはこれのインデックスがScriptIdに入る。
        /// </summary>
        public static readonly IReadOnlyList<string> Defaults;

        public AnimationEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public AnimationEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public AnimationEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes, data)
        {
        }

        static AnimationEffect()
        {
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
