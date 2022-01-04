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
        /// ファイル名の最大バイト長。
        /// </summary>
        public readonly int MaxFilename = 260;

        /// <summary>
        /// プロジェクトファイルに含まれる FilterConfigFile の最大個数。
        /// </summary>
        public readonly int MaxConfigFiles = 96;

        /// <summary>
        /// プロジェクトファイルに含まれる画像の最大個数。
        /// </summary>
        public readonly int MaxImages = 256;

        /// <summary>
        /// プロジェクトファイルの <see cref="EditHandle"/>。
        /// </summary>
        public EditHandle EditHandle { get; set; }

        /// <summary>
        /// 各フレームの情報。
        /// </summary>
        public readonly List<FrameData> Frames = new List<FrameData>();

        /// <summary>
        /// プロジェクトファイルに含まれている FilterConfigFile。
        /// </summary>
        public readonly List<byte[]> ConfigFiles = new List<byte[]>();

        /// <summary>
        /// プロジェクトファイルに含まれている画像。
        /// </summary>
        public readonly List<byte[]> Images = new List<byte[]>();

        /// <summary>
        /// 画像セクションとフッターの間にある未知のデータ。
        /// 通常このリストの長さは0です。
        /// </summary>
        public byte[] DataBeforeFooter { get; set; }

        /// <summary>
        /// プロジェクトファイルに含まれているフィルタプラグインのデータ。
        /// </summary>
        public readonly List<FilterProject> FilterProjects = new List<FilterProject>();

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
            using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
            {
                Read(reader);
            }
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

            EditHandle = new EditHandle(reader);
            var frameNum = reader.ReadInt32();

            var videos = AupUtil.DecompressUInt32Array(reader, frameNum);
            var audios = AupUtil.DecompressUInt32Array(reader, frameNum);
            var array2 = AupUtil.DecompressUInt32Array(reader, frameNum);
            var array3 = AupUtil.DecompressUInt32Array(reader, frameNum);
            var inters = AupUtil.DecompressUInt8Array(reader, frameNum);
            var index24Fps = AupUtil.DecompressUInt8Array(reader, frameNum);
            var editFlags = AupUtil.DecompressUInt8Array(reader, frameNum);
            var configs = AupUtil.DecompressUInt8Array(reader, frameNum);
            var vcms = AupUtil.DecompressUInt8Array(reader, frameNum);
            var array9 = AupUtil.DecompressUInt8Array(reader, frameNum);
            Frames.Clear();
            for (int i = 0; i < frameNum; i++)
            {
                Frames.Add(new FrameData()
                {
                    Video = videos[i],
                    Audio = audios[i],
                    Field2 = array2[i],
                    Field3 = array3[i],
                    Inter = inters[i],
                    Index24Fps = index24Fps[i],
                    EditFlag = editFlags[i],
                    Config = configs[i],
                    Vcm = vcms[i],
                    Field9 = array9[i],
                });
            }

            ConfigFiles.Clear();
            foreach (var name in EditHandle.ConfigNames)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    var size = reader.ReadInt32();
                    ConfigFiles.Add(reader.ReadBytes(size));
                }
            }
            Images.Clear();
            foreach (var handle in EditHandle.ImageHandles)
            {
                if (handle != 0)
                {
                    var size = reader.ReadInt32();
                    Images.Add(reader.ReadBytes(size));
                }
            }


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
            writer.Write(Frames.Count);
            AupUtil.CompressUInt32Array(writer, Frames.Select(f => f.Video).ToArray());
            AupUtil.CompressUInt32Array(writer, Frames.Select(f => f.Audio).ToArray());
            AupUtil.CompressUInt32Array(writer, Frames.Select(f => f.Field2).ToArray());
            AupUtil.CompressUInt32Array(writer, Frames.Select(f => f.Field3).ToArray());
            AupUtil.Comp(writer, Frames.Select(f => f.Inter).ToArray());
            AupUtil.Comp(writer, Frames.Select(f => f.Index24Fps).ToArray());
            AupUtil.Comp(writer, Frames.Select(f => f.EditFlag).ToArray());
            AupUtil.Comp(writer, Frames.Select(f => f.Config).ToArray());
            AupUtil.Comp(writer, Frames.Select(f => f.Vcm).ToArray());
            AupUtil.Comp(writer, Frames.Select(f => f.Field9).ToArray());

            foreach (var config in ConfigFiles)
            {
                writer.Write(config.Length);
                writer.Write(config);
            }
            foreach (var image in Images)
            {
                writer.Write(image.Length);
                writer.Write(image);
            }

            writer.Write(DataBeforeFooter);
            writer.Write(Header.ToSjisBytes());

            foreach (var filter in FilterProjects)
            {
                filter.Write(writer);
            }
        }

        byte[] SkipToFooter(BinaryReader reader)
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
