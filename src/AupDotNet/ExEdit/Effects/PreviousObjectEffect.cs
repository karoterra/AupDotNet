using System;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 直前オブジェクト
    /// </summary>
    public class PreviousObjectEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.PreviousObject;

        public PreviousObjectEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public PreviousObjectEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
