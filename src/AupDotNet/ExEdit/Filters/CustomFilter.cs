namespace Karoterra.AupDotNet.ExEdit.Filters
{
    public class CustomFilter : Filter
    {
        public byte[] Data { get; set; }

        private Trackbar[] _trackbars;
        public override Trackbar[] Trackbars => _trackbars;

        private uint[] _checkboxes;
        public override uint[] Checkboxes => _checkboxes;

        public CustomFilter(FilterType type, FilterFlag flag, Trackbar[] trackbars, uint[] checkboxes, byte[] data)
            : base(type)
        {
            Flag = flag;
            _trackbars = trackbars;
            _checkboxes = checkboxes;
            Data = data;
        }

        public override byte[] DumpExtData()
        {
            return Data;
        }
    }
}
