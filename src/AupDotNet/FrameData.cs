namespace Karoterra.AupDotNet
{
    public class FrameData
    {
        public uint Video { get; set; }
        public uint Audio { get; set; }
        public uint Field2 { get; set; }
        public uint Field3 { get; set; }
        public byte Inter { get; set; }
        public byte Index24Fps { get; set; }
        public byte EditFlag { get; set; }
        public byte Config { get; set; }
        public byte Vcm { get; set; }
        public byte Field9 { get; set; }

        public FrameData()
        {
        }

        public override bool Equals(object obj)
        {
            FrameData frame = obj as FrameData;
            return frame != null &&
                Video == frame.Video && Audio == frame.Audio &&
                Field2 == frame.Field2 && Field3 == frame.Field3 &&
                Inter == frame.Inter && Index24Fps == frame.Index24Fps &&
                EditFlag == frame.EditFlag && Config == frame.Config &&
                Vcm == frame.Vcm && Field9 == frame.Field9;
        }

        public override int GetHashCode()
        {
            return (int)Video ^ (int)Audio ^ (int)Field2 ^ (int)Field3 ^
                Inter ^ (Index24Fps << 8) ^ (EditFlag << 16) ^ (Config << 24) ^
                Vcm ^ (Field9 << 8);
        }
    }
}
