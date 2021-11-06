using System.Collections.Generic;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// カスタムオブジェクト
    /// </summary>
    public class CustomObjectEffect : ScriptFileEffect
    {
        private const int Id = (int)EffectTypeId.CustomObject;

        /// <summary>
        /// exedit.objで定義されているスクリプト。
        /// Nameが空の時にはこれのインデックスがScriptIdに入る。
        /// </summary>
        public static readonly IReadOnlyList<string> Defaults;

        public CustomObjectEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public CustomObjectEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public CustomObjectEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes, data)
        {
        }

        static CustomObjectEffect()
        {
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
