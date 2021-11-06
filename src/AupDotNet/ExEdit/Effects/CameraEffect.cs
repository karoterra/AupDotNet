using System.Collections.Generic;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// カメラ効果
    /// </summary>
    public class CameraEffect : ScriptFileEffect
    {
        private const int Id = (int)EffectTypeId.CameraEffect;

        /// <summary>
        /// exedit.camで定義されているスクリプト。
        /// Nameが空の時にはこれのインデックスがScriptIdに入る。
        /// </summary>
        public static readonly IReadOnlyList<string> Defaults;

        public CameraEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public CameraEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public CameraEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes, data)
        {
        }

        static CameraEffect()
        {
            Defaults = new string[]
            {
                "手ぶれ",
                "目標中心回転",
                "目標サイズ固定視野角",
            };
        }
    }
}
