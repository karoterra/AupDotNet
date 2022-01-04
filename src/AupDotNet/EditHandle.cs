using System;
using System.Collections.Generic;
using System.IO;
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
        public byte[] Data { get; set; }

        /// <summary>
        /// フラグ
        /// </summary>
        public uint Flag { get; set; }

        private string _editFilename;
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

        private string _outputFilename;
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

        private string _projectFilename;
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
        /// フレームの横幅。
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// フレームの高さ。
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// フレーム数。
        /// </summary>
        public int FrameNum { get; set; }

        /// <summary>
        /// 選択中フレームの開始フレーム。
        /// </summary>
        public int SelectedFrameStart { get; set; }

        /// <summary>
        /// 選択中フレームの終了フレーム。
        /// </summary>
        public int SelectedFrameEnd { get; set; }

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
        /// プロジェクトファイルに含まれる FilterConfigFile の名前。
        /// </summary>
        public readonly List<string> ConfigNames = new List<string>(MaxConfigFiles);

        /// <summary>
        /// プロジェクトファイルに含まれる画像のハンドル。
        /// </summary>
        public readonly List<uint> ImageHandles = new List<uint>(MaxImages);

        /// <summary>
        /// 新しい <see cref="EditHandle"/> のインスタンスを初期化します。
        /// </summary>
        public EditHandle()
        {
            Data = new byte[Size - UncompressedSize];
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
            Data = new byte[Size - UncompressedSize];
            AupUtil.Decomp(reader, Data);
            var span = new ReadOnlySpan<byte>(Data);

            ProjectFilename = span.Slice(0, MaxFilename).ToCleanSjisString();
            Width = span.Slice(0x310 - UncompressedSize, 4).ToInt32();
            Height = span.Slice(0x314 - UncompressedSize, 4).ToInt32();
            FrameNum = span.Slice(0x318 - UncompressedSize, 4).ToInt32();
            SelectedFrameStart = span.Slice(0x31c - UncompressedSize, 4).ToInt32();
            SelectedFrameEnd = span.Slice(0x320 - UncompressedSize, 4).ToInt32();
            CurrentFrame = span.Slice(0x330 - UncompressedSize, 4).ToInt32();
            VideoDecodeBit = span.Slice(0x3de - UncompressedSize, 2).ToInt16();
            VideoDecodeFormat = span.Slice(0x3e0 - UncompressedSize, 4).ToUInt32();
            AudioCh = span.Slice(0x3fa - UncompressedSize, 2).ToInt16();
            AudioRate = span.Slice(0x3fc - UncompressedSize, 4).ToInt32();
            VideoScale = span.Slice(0x468 - UncompressedSize, 4).ToInt32();
            VideoRate = span.Slice(0x46c - UncompressedSize, 4).ToInt32();

            ConfigNames.Clear();
            for (int i = 0; i < MaxConfigFiles; i++)
            {
                ConfigNames.Add(
                    span.Slice(0x20d18 - UncompressedSize + i * MaxFilename, MaxFilename)
                        .ToCleanSjisString()
                );
            }
            ImageHandles.Clear();
            for (int i = 0; i < MaxImages; i++)
            {
                ImageHandles.Add(span.Slice(0x4bbd98 - UncompressedSize + i * 4, 4).ToUInt32());
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
            FrameNum.ToBytes().CopyTo(span.Slice(0x318 - UncompressedSize, 4));
            SelectedFrameStart.ToBytes().CopyTo(span.Slice(0x31c - UncompressedSize, 4));
            SelectedFrameEnd.ToBytes().CopyTo(span.Slice(0x320 - UncompressedSize, 4));
            CurrentFrame.ToBytes().CopyTo(span.Slice(0x330 - UncompressedSize, 4));
            VideoDecodeBit.ToBytes().CopyTo(span.Slice(0x3de - UncompressedSize, 2));
            VideoDecodeFormat.ToBytes().CopyTo(span.Slice(0x3e0 - UncompressedSize, 4));
            AudioCh.ToBytes().CopyTo(span.Slice(0x3fa - UncompressedSize, 2));
            AudioRate.ToBytes().CopyTo(span.Slice(0x3fc - UncompressedSize, 4));
            VideoScale.ToBytes().CopyTo(span.Slice(0x468 - UncompressedSize, 4));
            VideoRate.ToBytes().CopyTo(span.Slice(0x46c - UncompressedSize, 4));

            for (int i = 0; i < MaxConfigFiles && i < ConfigNames.Count; i++)
            {
                ConfigNames[i].ToSjisBytes(MaxFilename)
                    .CopyTo(span.Slice(0x20d18 - UncompressedSize + i * MaxFilename));
            }
            for (int i = 0; i < MaxImages && i < ImageHandles.Count; i++)
            {
                ImageHandles[i].ToBytes()
                    .CopyTo(span.Slice(0x4bbd98 - UncompressedSize + i * 4));
            }

            AupUtil.Comp(writer, Data);
        }
    }
}
