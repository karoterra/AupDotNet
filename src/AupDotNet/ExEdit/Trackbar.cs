namespace Karoterra.AupDotNet.ExEdit
{
    public class Trackbar
    {
        public int Current { get; set; }
        public int Next { get; set; }
        public TrackbarFlag Flag { get; set; }
        public int ScriptIndex { get; set; }
        public int Parameter { get; set; }

        public uint Transition
        {
            get => (uint)Flag + (uint)(ScriptIndex << 16);
        }

        public Trackbar() { }

        public Trackbar(int current, int next, uint transition, int param)
        {
            Current = current;
            Next = next;
            Flag = (TrackbarFlag)(transition & 0xFF);
            ScriptIndex = (int)(transition >> 16);
            Parameter = param;
        }
    }
}
