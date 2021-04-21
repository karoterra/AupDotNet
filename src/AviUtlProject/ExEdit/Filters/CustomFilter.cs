namespace Karoterra.AviUtlProject.ExEdit.Filters
{
    public class CustomFilter : Filter
    {
        public byte[] Data { get; set; }
        public CustomFilter(FilterType type, FilterFlag flag, Trackbar[] trackbars, byte[] data)
        {
            Type = type;
            Flag = flag;
            Trackbars = trackbars;
            Data = data;
        }

        public override byte[] DumpExtData()
        {
            return Data;
        }
    }
}
