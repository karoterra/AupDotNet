using System;
using System.Linq;

namespace Karoterra.AupDotNet.ExEdit
{
    public abstract class Effect
    {
        public readonly EffectType Type;
        public EffectFlag Flag { get; set; }

        public Trackbar[] Trackbars { get; }

        public int[] Checkboxes { get; }

        public Effect(EffectType type)
        {
            Type = type;
            Trackbars = new Trackbar[Type.TrackbarNum];
            foreach (var (def, i) in type.Trackbars.Select((def, i) => (def, i)))
            {
                if (def == null)
                    Trackbars[i] = new Trackbar();
                else
                    Trackbars[i] = new Trackbar(def.Default, def.Default, 0, 0);
            }
            Checkboxes = new int[Type.CheckboxNum];
            foreach (var (def, i) in type.Checkboxes
                .Select((def, i) => (def, i))
                .Where(x => x.def != null && x.def.IsCheckbox))
            {
                Checkboxes[i] = def.Default;
            }
        }

        public Effect(EffectType type, Trackbar[] trackbars, int[] checkboxes)
        {
            Type = type;
            if (trackbars.Length != type.TrackbarNum)
            {
                throw new ArgumentException("Trackbars' length is invalid");
            }
            Trackbars = trackbars.Clone() as Trackbar[];
            if (checkboxes.Length != type.CheckboxNum)
            {
                throw new ArgumentException("Checkboxes' length is invalid");
            }
            Checkboxes = checkboxes.Clone() as int[];
        }

        public abstract byte[] DumpExtData();
    }
}
