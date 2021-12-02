namespace Karoterra.AupDotNet.ExEdit
{
    public class TrackbarDefinition
    {
        public string Name { get; set; }
        public int Scale { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Default { get; set; }

        public TrackbarDefinition(string name, int scale, int min, int max, int def)
        {
            Name = name;
            Scale = scale;
            Min = min;
            Max = max;
            Default = def;
        }
    }
}
