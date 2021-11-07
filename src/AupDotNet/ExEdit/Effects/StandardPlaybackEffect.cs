using System;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// 標準再生
    /// </summary>
    public class StandardPlaybackEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.StandardPlayback;

        public Trackbar Volume => Trackbars[0];
        public Trackbar Pan => Trackbars[1];

        public StandardPlaybackEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public StandardPlaybackEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }
    }
}
