using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet
{
    /// <summary>
    /// フィルタプラグインのデータを表すクラス用の抽象クラス。
    /// </summary>
    public abstract class FilterProject
    {
        /// <summary>
        /// フィルタプラグインのデータセクションのヘッダ。
        /// </summary>
        protected static readonly string Header = "FilterProject 0.1\0";

        /// <summary>
        /// フィルタプラグインの名前。
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// フィルタプラグインのデータをダンプします。
        /// </summary>
        /// <returns>フィルタプラグインのデータ</returns>
        public abstract byte[] DumpData();

        /// <summary>
        /// 指定したライタにフィルタプラグインのデータを書き込みます。
        /// </summary>
        /// <param name="writer">ライタ</param>
        public void Write(BinaryWriter writer)
        {
            writer.Write(Header.ToSjisBytes());
            var name = Name.ToSjisBytes();
            writer.Write(name.Length + 1);
            writer.Write(name);
            writer.Write((byte)0);
            var data = DumpData();
            writer.Write(data.Length);
            AupUtil.Comp(writer, data);
        }
    }
}
