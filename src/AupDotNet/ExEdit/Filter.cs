namespace Karoterra.AupDotNet.ExEdit
{
    public abstract class Filter
    {
        public readonly FilterType Type;
        public FilterFlag Flag { get; set; }
        public virtual Trackbar[] Trackbars { get; }
        public virtual uint[] Checkboxes { get; }

        public Filter(FilterType filterType)
        {
            Type = filterType;
        }

        public abstract byte[] DumpExtData();
    }
}
