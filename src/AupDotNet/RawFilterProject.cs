using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet
{
    /// <summary>
    /// フィルタプラグインのデータをそのまま <c>byte</c> 配列として保持する <see cref="FilterProject"/>。
    /// </summary>
    public class RawFilterProject : FilterProject
    {
        /// <summary>
        /// フィルタプラグインのデータ
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// 名前とデータを指定して <see cref="RawFilterProject"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="name">フィルタプラグインの名前</param>
        /// <param name="data">フィルタプラグインのデータ</param>
        public RawFilterProject(string name, byte[] data)
        {
            Name = name;
            Data = data;
        }

        /// <summary>
        /// 指定したリーダから <see cref="RawFilterProject"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="reader">リーダ</param>
        /// <exception cref="FileFormatException">フィルタプラグインのデータではありません。</exception>
        public RawFilterProject(BinaryReader reader)
        {
            var header = reader.ReadBytes(18).ToSjisString();
            if(header != Header)
            {
                throw new FileFormatException("Cannot find FilterProject header.");
            }
            var nameLength = reader.ReadInt32();
            Name = reader.ReadBytes(nameLength).ToCleanSjisString();
            var dataSize = reader.ReadInt32();
            Data = new byte[dataSize];
            AupUtil.Decomp(reader, Data);
        }

        public override byte[] DumpData()
        {
            return Data;
        }
    }
}
