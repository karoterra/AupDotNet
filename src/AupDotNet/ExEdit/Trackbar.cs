using System;

namespace Karoterra.AupDotNet.ExEdit
{
    public enum TrackbarType
    {
        Stop = 0,
        Liner = 1,
        Curve = 2,
        Step = 3,
        IgnoreKeyframe = 4,
        Movement = 5,
        Random = 6,
        AccelDecel = 7,
        Repeat = 8,
        Script = 0xF,
    }

    [Flags]
    public enum TrackbarFlag
    {
        Deceleration = 0x20,
        Acceleration = 0x40,
    }

    public class Trackbar
    {
        public int Current { get; set; }
        public int Next { get; set; }
        public TrackbarType Type { get; set; }
        public TrackbarFlag Flag { get; set; }
        public int ScriptIndex { get; set; }
        public int Parameter { get; set; }

        public uint Transition
        {
            get => (uint)Type +  (uint)Flag + (uint)(ScriptIndex << 16);
        }

        public Trackbar() { }

        public Trackbar(int current, int next, uint transition, int param)
        {
            Current = current;
            Next = next;
            Type = (TrackbarType)(transition & 0xF);
            Flag = (TrackbarFlag)(transition & 0xF0);
            ScriptIndex = (int)(transition >> 16);
            Parameter = param;
        }

        public string ToString(int scale, TrackbarScript script)
        {
            string current, next;
            switch (scale)
            {
                case 100:
                    current = $"{Current / 100.0:F2}";
                    next = $",{Next / 100.0:F2}";
                    break;
                case 10:
                    current = $"{Current / 10.0:F1}";
                    next = $",{Next / 10.0:F1}";
                    break;
                default:
                    current = Current.ToString();
                    next = $",{Next}";
                    break;
            }
            if (Type == TrackbarType.Stop)
                return current;

            if (Type != TrackbarType.Script)
                script = TrackbarScript.BuiltIn[(int)Type];

            string transition = $",{(script.EnableSpeed ? ((int)Flag | (int)Type) : (int)Type)}";
            if (Type == TrackbarType.Script)
                transition += $"@{script.Name}";
            string param = script.EnableParam ? $",{Parameter}" : string.Empty;

            return $"{current}{next}{transition}{param}";
        }
    }
}
