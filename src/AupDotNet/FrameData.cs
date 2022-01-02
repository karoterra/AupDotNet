namespace Karoterra.AupDotNet
{
    /// <summary>
    /// フレームの情報を表すクラス。
    /// </summary>
    public class FrameData
    {
        /// <summary>
        /// 映像の参照フレーム。
        /// </summary>
        public uint Video { get; set; }

        /// <summary>
        /// 音声の参照フレーム。
        /// </summary>
        public uint Audio { get; set; }

        /// <summary>
        /// フレーム情報2。
        /// </summary>
        public uint Field2 { get; set; }

        /// <summary>
        /// フレーム情報3。
        /// </summary>
        public uint Field3 { get; set; }

        /// <summary>
        /// Inter.
        /// </summary>
        public byte Inter { get; set; }

        /// <summary>
        /// Index 24 fps.
        /// </summary>
        public byte Index24Fps { get; set; }

        /// <summary>
        /// Edit flag.
        /// </summary>
        public byte EditFlag { get; set; }

        /// <summary>
        /// Config.
        /// </summary>
        public byte Config { get; set; }

        /// <summary>
        /// VCM.
        /// </summary>
        public byte Vcm { get; set; }

        /// <summary>
        /// フレーム情報9。
        /// </summary>
        public byte Field9 { get; set; }

        /// <summary>
        /// <see cref="FrameData"/> のインスタンスを初期化します。
        /// </summary>
        public FrameData()
        {
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is FrameData frame &&
                Video == frame.Video && Audio == frame.Audio &&
                Field2 == frame.Field2 && Field3 == frame.Field3 &&
                Inter == frame.Inter && Index24Fps == frame.Index24Fps &&
                EditFlag == frame.EditFlag && Config == frame.Config &&
                Vcm == frame.Vcm && Field9 == frame.Field9;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return (int)Video ^ (int)Audio ^ (int)Field2 ^ (int)Field3 ^
                Inter ^ (Index24Fps << 8) ^ (EditFlag << 16) ^ (Config << 24) ^
                Vcm ^ (Field9 << 8);
        }
    }
}
