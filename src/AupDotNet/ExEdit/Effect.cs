using System;
using System.Collections.ObjectModel;

namespace Karoterra.AupDotNet.ExEdit
{
    public abstract class Effect
    {
        public readonly EffectType Type;
        public EffectFlag Flag { get; set; }

        private Trackbar[] _trackbars;
        public ReadOnlyCollection<Trackbar> Trackbars { get; }

        public int[] Checkboxes { get; }

        public Effect(EffectType type)
        {
            Type = type;
            _trackbars = new Trackbar[Type.TrackbarNum];
            for (int i = 0; i < Type.TrackbarNum; i++)
            {
                _trackbars[i] = new Trackbar();
            }
            Trackbars = new ReadOnlyCollection<Trackbar>(_trackbars);
            Checkboxes = new int[Type.CheckboxNum];
        }

        public Effect(EffectType type, Trackbar[] trackbars, int[] checkboxes)
        {
            Type = type;
            if (trackbars.Length != type.TrackbarNum)
            {
                throw new ArgumentException("Trackbars' length is invalid");
            }
            _trackbars = trackbars;
            Trackbars = new ReadOnlyCollection<Trackbar>(_trackbars);
            if (checkboxes.Length != type.CheckboxNum)
            {
                throw new ArgumentException("Checkboxes' length is invalid");
            }
            Checkboxes = checkboxes;
        }

        public abstract byte[] DumpExtData();
    }
}
