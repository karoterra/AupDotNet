using System;
using System.Collections.Generic;
using System.IO;
using Karoterra.AupDotNet.Extensions;


namespace Karoterra.AupDotNet
{
    public class EditHandle
    {
        public static readonly int Size = 0x4c09e8;
        public static readonly int UncompressedSize = 0x20c;
        public static readonly int MaxFilename = 260;
        public static readonly int MaxConfigFiles = 96;
        public static readonly int MaxImages = 256;

        public byte[] Data { get; set; }

        public uint Flag { get; set; }

        private string _editFilename;
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

        public int Width { get; set; }

        public int Height { get; set; }

        public int FrameNum { get; set; }

        public int SelectedFrameStart { get; set; }

        public int SelectedFrameEnd { get; set; }

        public int CurrentFrame { get; set; }

        public short VideoDecodeBit { get; set; }

        public uint VideoDecodeFormat { get; set; }

        public short AudioCh { get; set; }

        public int AudioRate { get; set; }

        public int VideoScale { get; set; }

        public int VideoRate { get; set; }

        public readonly List<string> ConfigNames = new List<string>(MaxConfigFiles);

        public readonly List<uint> ImageHandles = new List<uint>(MaxImages);

        public EditHandle()
        {
            Data = new byte[Size - UncompressedSize];
        }

        public EditHandle(BinaryReader reader)
        {
            Read(reader);
        }

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
