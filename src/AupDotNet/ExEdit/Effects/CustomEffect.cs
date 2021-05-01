namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public class CustomEffect : Effect
    {
        public byte[] Data { get; set; }

        private Trackbar[] _trackbars;
        public override Trackbar[] Trackbars => _trackbars;

        private uint[] _checkboxes;
        public override uint[] Checkboxes => _checkboxes;

        public CustomEffect(EffectType type, EffectFlag flag, Trackbar[] trackbars, uint[] checkboxes, byte[] data)
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
