using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet
{
    /// <summary>
    /// AviUtl プロジェクトファイルを表すクラス。
    /// </summary>
    public class AviUtlProject
    {
        const string Header = "AviUtl ProjectFile version 0.18\0";

        /// <summary>
        /// プロジェクトファイルの <see cref="EditHandle"/>。
        /// </summary>
        public EditHandle EditHandle { get; set; } = new();

        /// <summary>
        /// 画像セクションとフッターの間にある未知のデータ。
        /// 通常このリストの長さは0です。
        /// </summary>
        public byte[] DataBeforeFooter { get; set; } = Array.Empty<byte>();

        /// <summary>
        /// プロジェクトファイルに含まれているフィルタプラグインのデータ。
        /// </summary>
        public readonly List<FilterProject> FilterProjects = new();

        /// <summary>
        /// 空のプロジェクトファイルを表す新しい <see cref="AviUtlProject"/> のインスタンスを初期化します。
        /// </summary>
        public AviUtlProject()
        {
        }

        /// <summary>
        /// 指定したリーダからプロジェクトファイルを読み込んで <see cref="AviUtlProject"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="reader">プロジェクトファイルを読み込むリーダ</param>
        /// <exception cref="FileFormatException">AviUtl プロジェクトファイルとして読み込むことができません。</exception>
        public AviUtlProject(BinaryReader reader)
        {
            Read(reader);
        }

        /// <summary>
        /// 指定したファイルからプロジェクトファイルを読み込んで <see cref="AviUtlProject"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="path">ファイル名</param>
        /// <exception cref="FileFormatException">AviUtl プロジェクトファイルとして読み込むことができません。</exception>
        public AviUtlProject(string path)
        {
            using BinaryReader reader = new(File.OpenRead(path));
            Read(reader);
        }

        /// <summary>
        /// 指定したリーダからプロジェクトファイルを読み込みます。
        /// </summary>
        /// <param name="reader">プロジェクトファイルを読み込むリーダ</param>
        /// <exception cref="FileFormatException">AviUtl プロジェクトファイルとして読み込むことができません。</exception>
        public void Read(BinaryReader reader)
        {
            var baseStream = reader.BaseStream;

            var header = reader.ReadBytes(Header.GetSjisByteCount()).ToSjisString();
            if (header != Header)
            {
                throw new FileFormatException("Cannot find AviUtl ProjectFile header.");
            }

            EditHandle.Read(reader);

            DataBeforeFooter = SkipToFooter(reader);

            FilterProjects.Clear();
            while (baseStream.Position != baseStream.Length)
            {
                var filter = new RawFilterProject(reader);
                FilterProjects.Add(filter);
            }
        }

        /// <summary>
        /// 指定したライタにプロジェクトファイルを書き込みます。
        /// </summary>
        /// <param name="writer">プロジェクトファイルを書き込むライタ</param>
        public void Write(BinaryWriter writer)
        {
            writer.Write(Header.ToSjisBytes());
            EditHandle.Write(writer);

            writer.Write(DataBeforeFooter);
            writer.Write(Header.ToSjisBytes());

            foreach (var filter in FilterProjects)
            {
                filter.Write(writer);
            }
        }

        /// <summary>
        /// リーダーをフッターが見つかるまで進めます。
        /// </summary>
        /// <param name="reader">AviUtl プロジェクトファイルを読み込むリーダー</param>
        /// <returns>フッターの前のデータ</returns>
        protected static byte[] SkipToFooter(BinaryReader reader)
        {
            int index = 0;
            var footer = Header.ToSjisBytes();
            var skippedData = new List<byte>();
            while (true)
            {
                byte data = reader.ReadByte();
                skippedData.Add(data);
                if (data == footer[index])
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
                if (index == footer.Length)
                {
                    skippedData.RemoveRange(skippedData.Count - footer.Length, footer.Length);
                    return skippedData.ToArray();
                }
            }
        }
    }
}
