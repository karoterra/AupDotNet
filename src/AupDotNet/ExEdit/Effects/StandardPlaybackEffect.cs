using System;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public class StandardPlaybackEffect : NoExtDataEffect
    {
        private const int Id = (int)EffectTypeId.StandardPlayback;

        public Trackbar Volume => Trackbars[0];
        public Trackbar Pan => Trackbars[0];

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
