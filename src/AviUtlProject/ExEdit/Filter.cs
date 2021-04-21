namespace Karoterra.AviUtlProject.ExEdit
{
    public abstract class Filter
    {
        public FilterType Type { get; set; }
        public FilterFlag Flag { get; set; }
        public Trackbar[] Trackbars { get; set; }

        public abstract byte[] DumpExtData();
    }
}
