using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Karoterra.AupDotNet.Extensions;


namespace Karoterra.AupDotNet
{
    /// <summary>
    /// AviUtl で扱われる EditHandle を表すクラス。
    /// </summary>
    public class EditHandle
    {
        /// <summary>
        /// 圧縮前の EditHandle のバイト長。
        /// </summary>
        public static readonly int Size = 0x4c09e8;

        /// <summary>
        /// EditHandle の先頭の圧縮しない部分のバイト長。
        /// </summary>
        public static readonly int UncompressedSize = 0x20c;

        /// <summary>
        /// ファイル名の最大バイト長。
        /// </summary>
        public static readonly int MaxFilename = 260;

        /// <summary>
        /// プロジェクトファイルに含まれる FilterConfigFile の最大個数。
        /// </summary>
        public static readonly int MaxConfigFiles = 96;

        /// <summary>
        /// プロジェクトファイルに含まれる画像の最大個数。
        /// </summary>
        public static readonly int MaxImages = 256;

        /// <summary>
        /// EditHandle のデータ
        /// </summary>
        public byte[] Data { get; } = new byte[Size - UncompressedSize];

        /// <summary>
        /// フラグ
        /// </summary>
        public uint Flag { get; set; }

        private string _editFilename = string.Empty;
        /// <summary>
        /// 編集中のファイル名。
        /// </summary>
        /// <remarks>
        /// 長さの上限は Shift JIS エンコーディングで <see cref="MaxFilename"/> バイトです。
        /// </remarks>
        public string EditFilename
        {
            get => _editFilename;
            set
            {
                if (value.GetSjisByteCount() >= MaxFilename)
                {
                    throw new MaxByteCountOfStringException(nameof(EditFilename), MaxFilename);
                }
                _editFilename = value;
            }
        }

        private string _outputFilename = string.Empty;
        /// <summary>
        /// 出力ファイル名。
        /// </summary>
        /// <remarks>
        /// 長さの上限は Shift JIS エンコーディングで <see cref="MaxFilename"/> バイトです。
        /// </remarks>
        public string OutputFilename
        {
            get => _outputFilename;
            set
            {
                if (value.GetSjisByteCount() >= MaxFilename)
                {
                    throw new MaxByteCountOfStringException(nameof(OutputFilename), MaxFilename);
                }
                _outputFilename = value;
            }
        }

        private string _projectFilename = string.Empty;
        /// <summary>
        /// プロジェクトファイル名。
        /// </summary>
        /// <remarks>
        /// 長さの上限は Shift JIS エンコーディングで <see cref="MaxFilename"/> バイトです。
        /// </remarks>
        public string ProjectFilename
        {
            get => _projectFilename;
            set
            {
                if (value.GetSjisByteCount() >= MaxFilename)
                {
                    throw new MaxByteCountOfStringException(nameof(ProjectFilename), MaxFilename);
                }
                _projectFilename = value;
            }
        }

        /// <summary>
        /// フレームのサイズ変更前の横幅。
        /// </summary>
        /// <remarks>
        /// プロジェクト作成時の横幅。
        /// </remarks>
        public int Width { get; set; }

        /// <summary>
        /// フレームのサイズ変更前の高さ。
        /// </summary>
        /// <remarks>
        /// プロジェクト作成時の高さ。
        /// </remarks>
        public int Height { get; set; }

        /// <summary>
        /// 選択中フレームの開始フレーム。
        /// </summary>
        public int SelectedFrameStart { get; set; }

        /// <summary>
        /// 選択中フレームの終了フレーム。
        /// </summary>
        public int SelectedFrameEnd { get; set; }

        /// <summary>
        /// フレームのサイズ変更後の横幅。
        /// </summary>
        /// <remarks>
        /// AviUtlの「サイズの変更」やサイズ変更系のフィルタープラグインを使用してサイズを変更した場合の横幅。
        /// 使用していない(変更していない)場合プロジェクト作成時の横幅と同じになる。
        /// </remarks>
        public int ResizedWidth { get; set; }

        /// <summary>
        /// フレームのサイズ変更後の高さ。
        /// </summary>
        /// <remarks>
        /// AviUtlの「サイズの変更」やサイズ変更系のフィルタープラグインを使用してサイズを変更した場合の高さ。
        /// 使用していない(変更していない)場合プロジェクト作成時の高さと同じになる。
        /// </remarks>
        public int ResizedHeight { get; set; }

        /// <summary>
        /// 現在表示中のフレーム。
        /// </summary>
        public int CurrentFrame { get; set; }

        /// <summary>
        /// ビデオ展開形式のビット数
        /// </summary>
        public short VideoDecodeBit { get; set; }

        /// <summary>
        /// ビデオ展開形式
        /// </summary>
        public uint VideoDecodeFormat { get; set; }

        /// <summary>
        /// 音声のチャンネル数。
        /// </summary>
        public short AudioCh { get; set; }

        /// <summary>
        /// 音声のサンプリングレート。
        /// </summary>
        public int AudioRate { get; set; }

        /// <summary>
        /// 映像のフレームレート(分母)。
        /// </summary>
        public int VideoScale { get; set; }

        /// <summary>
        /// 映像のフレームレート(分子)。
        /// </summary>
        public int VideoRate { get; set; }

        /// <summary>
        /// 各フレームの情報。
        /// </summary>
        public List<FrameStatus> Frames { get; } = new();

        /// <summary>
        /// プロジェクトファイルに含まれる FilterConfigFile。
        /// </summary>
        public List<FilterConfig> FilterConfigs { get; } = new();

        /// <summary>
        /// クリップボードから貼り付けた画像。
        /// </summary>
        public ClippedImage?[] ClippedImages { get; } = new ClippedImage[MaxImages];

        /// <summary>
        /// 新しい <see cref="EditHandle"/> のインスタンスを初期化します。
        /// </summary>
        public EditHandle()
        {
        }

        /// <summary>
        /// 指定したリーダから <see cref="EditHandle"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="reader">リーダ</param>
        /// <exception cref="FileFormatException">EditHandle のデータではありません。</exception>
        public EditHandle(BinaryReader reader)
        {
            Read(reader);
        }

        /// <summary>
        /// 指定したリーダから EditHandle を読み込みます。
        /// </summary>
        /// <param name="reader">リーダ</param>
        /// <exception cref="FileFormatException">EditHandle のデータではありません。</exception>
        public void Read(BinaryReader reader)
        {
            int size = reader.ReadInt32();
            if (size != Size)
            {
                throw new FileFormatException($"Edit handle size must be {Size} byte.");
            }

            Flag = reader.ReadUInt32();
            EditFilename = reader.ReadBytes(MaxFilename).ToCleanSjisString();
            OutputFilename = reader.ReadBytes(MaxFilename).ToCleanSjisString();
            AupUtil.Decomp(reader, Data);
            var span = new ReadOnlySpan<byte>(Data);

            ProjectFilename = span.Slice(0, MaxFilename).ToCleanSjisString();
            Width = span.Slice(0x310 - UncompressedSize, 4).ToInt32();
            Height = span.Slice(0x314 - UncompressedSize, 4).ToInt32();
            SelectedFrameStart = span.Slice(0x31c - UncompressedSize, 4).ToInt32();
            SelectedFrameEnd = span.Slice(0x320 - UncompressedSize, 4).ToInt32();
            ResizedWidth = span.Slice(0x328 - UncompressedSize, 4).ToInt32();
            ResizedHeight = span.Slice(0x32c - UncompressedSize, 4).ToInt32();
            CurrentFrame = span.Slice(0x330 - UncompressedSize, 4).ToInt32();
            VideoDecodeBit = span.Slice(0x3de - UncompressedSize, 2).ToInt16();
            VideoDecodeFormat = span.Slice(0x3e0 - UncompressedSize, 4).ToUInt32();
            AudioCh = span.Slice(0x3fa - UncompressedSize, 2).ToInt16();
            AudioRate = span.Slice(0x3fc - UncompressedSize, 4).ToInt32();
            VideoScale = span.Slice(0x468 - UncompressedSize, 4).ToInt32();
            VideoRate = span.Slice(0x46c - UncompressedSize, 4).ToInt32();

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
                Frames.Add(new FrameStatus()
                {
                    Video = videos[i],
                    Audio = audios[i],
                    Field2 = array2[i],
                    Field3 = array3[i],
                    Inter = (FrameStatusInter)inters[i],
                    Index24Fps = index24Fps[i],
                    EditFlag = (EditFrameEditFlag)editFlags[i],
                    Config = configs[i],
                    Vcm = vcms[i],
                    Clip = array9[i],
                });
            }

            FilterConfigs.Clear();
            for (int i = 0; i < MaxConfigFiles; i++)
            {
                var name = span.Slice(0x20d18 - UncompressedSize + i * MaxFilename, MaxFilename)
                    .ToCleanSjisString();
                if (string.IsNullOrEmpty(name)) break;
                var configSize = reader.ReadInt32();
                var data = reader.ReadBytes(configSize);
                FilterConfigs.Add(new FilterConfig(name, data));
            }

            for (int i = 0; i < MaxImages; i++)
            {
                var handle = span.Slice(0x4bbd98 - UncompressedSize + i * 4, 4).ToUInt32();
                if (handle == ClippedImage.NoDataHandle)
                {
                    ClippedImages[i] = null;
                    continue;
                }
                var imageSize = reader.ReadInt32();
                var data = reader.ReadBytes(imageSize);
                ClippedImages[i] = new ClippedImage(handle, data);
            }
        }

        /// <summary>
        /// 指定したライタに EditHandle を書き込みます。
        /// </summary>
        /// <param name="writer">ライタ</param>
        public void Write(BinaryWriter writer)
        {
            writer.Write(Size);
            writer.Write(Flag);
            writer.Write(EditFilename.ToSjisBytes(MaxFilename));
            writer.Write(OutputFilename.ToSjisBytes(MaxFilename));

            var span = new Span<byte>(Data);
            ProjectFilename.ToSjisBytes(MaxFilename).CopyTo(span.Slice(0, MaxFilename));
            Width.ToBytes().CopyTo(span.Slice(0x310 - UncompressedSize, 4));
            Height.ToBytes().CopyTo(span.Slice(0x314 - UncompressedSize, 4));
            Frames.Count.ToBytes().CopyTo(span.Slice(0x318 - UncompressedSize, 4));
            SelectedFrameStart.ToBytes().CopyTo(span.Slice(0x31c - UncompressedSize, 4));
            SelectedFrameEnd.ToBytes().CopyTo(span.Slice(0x320 - UncompressedSize, 4));
            ResizedWidth.ToBytes().CopyTo(span.Slice(0x328 - UncompressedSize, 4));
            ResizedHeight.ToBytes().CopyTo(span.Slice(0x32c - UncompressedSize, 4));
            CurrentFrame.ToBytes().CopyTo(span.Slice(0x330 - UncompressedSize, 4));
            VideoDecodeBit.ToBytes().CopyTo(span.Slice(0x3de - UncompressedSize, 2));
            VideoDecodeFormat.ToBytes().CopyTo(span.Slice(0x3e0 - UncompressedSize, 4));
            AudioCh.ToBytes().CopyTo(span.Slice(0x3fa - UncompressedSize, 2));
            AudioRate.ToBytes().CopyTo(span.Slice(0x3fc - UncompressedSize, 4));
            VideoScale.ToBytes().CopyTo(span.Slice(0x468 - UncompressedSize, 4));
            VideoRate.ToBytes().CopyTo(span.Slice(0x46c - UncompressedSize, 4));

            int index = 0;
            foreach (var name in FilterConfigs
                .Select(x => x.Name)
                .Concat(Enumerable.Repeat(string.Empty, MaxConfigFiles))
                .Take(MaxConfigFiles))
            {
                name.ToSjisBytes(MaxFilename)
                    .CopyTo(span.Slice(0x20d18 - UncompressedSize + index * MaxFilename));
                index++;
            }
            index = 0;
            foreach (var handle in ClippedImages
                .Select(x => x?.Handle ?? ClippedImage.NoDataHandle))
            {
                handle.ToBytes().CopyTo(span.Slice(0x4bbd98 - UncompressedSize + index * 4));
                index++;
            }

            AupUtil.Comp(writer, Data);

            writer.Write(Frames.Count);
            AupUtil.CompressUInt32Array(writer, Frames.Select(f => f.Video).ToArray());
            AupUtil.CompressUInt32Array(writer, Frames.Select(f => f.Audio).ToArray());
            AupUtil.CompressUInt32Array(writer, Frames.Select(f => f.Field2).ToArray());
            AupUtil.CompressUInt32Array(writer, Frames.Select(f => f.Field3).ToArray());
            AupUtil.Comp(writer, Frames.Select(f => (byte)f.Inter).ToArray());
            AupUtil.Comp(writer, Frames.Select(f => f.Index24Fps).ToArray());
            AupUtil.Comp(writer, Frames.Select(f => (byte)f.EditFlag).ToArray());
            AupUtil.Comp(writer, Frames.Select(f => f.Config).ToArray());
            AupUtil.Comp(writer, Frames.Select(f => f.Vcm).ToArray());
            AupUtil.Comp(writer, Frames.Select(f => f.Clip).ToArray());

            foreach (var config in FilterConfigs.Take(MaxConfigFiles))
            {
                writer.Write(config.Data.Length);
                writer.Write(config.Data);
            }
            foreach (var image in ClippedImages)
            {
                if (image == null || image.Handle == ClippedImage.NoDataHandle)
                    continue;
                writer.Write(image.Data.Length);
                writer.Write(image.Data);
            }
        }
    }
}
