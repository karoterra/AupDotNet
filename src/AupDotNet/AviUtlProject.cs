using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet
{
    public class AviUtlProject
    {
        const string Header = "AviUtl ProjectFile version 0.18\0";
        public readonly int MaxFilename = 260;
        public readonly int MaxConfigFiles = 96;
        public readonly int MaxImages = 256;

        public uint HandleSize { get; set; }
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

        public byte[] EditHandleData { get; set; }

        public readonly List<FrameData> Frames = new List<FrameData>();

        public readonly List<byte[]> ConfigFiles = new List<byte[]>();
        public readonly List<byte[]> Images = new List<byte[]>();

        public byte[] DataBeforeFooter { get; set; }

        public readonly List<FilterProject> FilterProjects = new List<FilterProject>();

        public AviUtlProject()
        {
        }

        public AviUtlProject(BinaryReader reader)
        {
            Read(reader);
        }

        public AviUtlProject(string path)
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
            {
                Read(reader);
            }
        }

        public void Read(BinaryReader reader)
        {
            var baseStream = reader.BaseStream;

            var header = reader.ReadBytes(Header.GetSjisByteCount()).ToSjisString();
            if (header != Header)
            {
                throw new FileFormatException("Cannot find AviUtl ProjectFile header.");
            }

            HandleSize = reader.ReadUInt32();
            Flag = reader.ReadUInt32();
            EditFilename = reader.ReadBytes(MaxFilename).ToCleanSjisString();
            OutputFilename = reader.ReadBytes(MaxFilename).ToCleanSjisString();
            var buf = new byte[HandleSize - MaxFilename * 2 - 4];
            Decomp(reader, buf);
            ProjectFilename = new ReadOnlySpan<byte>(buf, 0, MaxFilename).ToCleanSjisString();
            EditHandleData = buf.Skip(MaxFilename).ToArray();
            var frameNum = reader.ReadInt32();

            var videos = ReadCompressedUInt32Array(reader, frameNum);
            var audios = ReadCompressedUInt32Array(reader, frameNum);
            var array2 = ReadCompressedUInt32Array(reader, frameNum);
            var array3 = ReadCompressedUInt32Array(reader, frameNum);
            var inters = ReadCompressedUInt8Array(reader, frameNum);
            var index24Fps = ReadCompressedUInt8Array(reader, frameNum);
            var editFlags = ReadCompressedUInt8Array(reader, frameNum);
            var configs = ReadCompressedUInt8Array(reader, frameNum);
            var array8 = ReadCompressedUInt8Array(reader, frameNum);
            var vcms = ReadCompressedUInt8Array(reader, frameNum);
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
                    Field8 = array8[i],
                    Vcm = vcms[i]
                });
            }

            var editHandleData = new ReadOnlySpan<byte>(EditHandleData);
            ConfigFiles.Clear();
            for (int i = 0; i < MaxConfigFiles; i++)
            {
                if (EditHandleData[0x20A08 + i * MaxFilename] != 0)
                {
                    var size = reader.ReadInt32();
                    ConfigFiles.Add(reader.ReadBytes(size));
                }
            }
            Images.Clear();
            for (int i = 0; i < MaxImages; i++)
            {
                if (editHandleData.Slice(0x4BBA88 + i * sizeof(int), sizeof(int)).ToInt32() != 0)
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

        public void Write(BinaryWriter writer)
        {
            writer.Write(Header.ToSjisBytes());
            writer.Write(HandleSize);
            writer.Write(Flag);
            writer.Write(EditFilename.ToSjisBytes(MaxFilename));
            writer.Write(OutputFilename.ToSjisBytes(MaxFilename));
            Comp(writer, ProjectFilename.ToSjisBytes(MaxFilename));
            Comp(writer, EditHandleData);
            writer.Write(Frames.Count);
            CompressUInt32Array(writer, Frames.Select(f => f.Video).ToArray());
            CompressUInt32Array(writer, Frames.Select(f => f.Audio).ToArray());
            CompressUInt32Array(writer, Frames.Select(f => f.Field2).ToArray());
            CompressUInt32Array(writer, Frames.Select(f => f.Field3).ToArray());
            Comp(writer, Frames.Select(f => f.Inter).ToArray());
            Comp(writer, Frames.Select(f => f.Index24Fps).ToArray());
            Comp(writer, Frames.Select(f => f.EditFlag).ToArray());
            Comp(writer, Frames.Select(f => f.Config).ToArray());
            Comp(writer, Frames.Select(f => f.Field8).ToArray());
            Comp(writer, Frames.Select(f => f.Vcm).ToArray());

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

        public static byte[] ReadCompressedUInt8Array(BinaryReader reader, int length)
        {
            var buf = new byte[length];
            Decomp(reader, buf);
            return buf;
        }

        public static uint[] ReadCompressedUInt32Array(BinaryReader reader, int length)
        {
            var buf = new byte[length * 4];
            Decomp(reader, buf);
            var array = new uint[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = BitConverter.ToUInt32(buf, i * 4);
            }
            return array;
        }

        public static void Decomp(BinaryReader reader, byte[] buf)
        {
            int index = 0;
            while (index < buf.Length)
            {
                byte size1 = reader.ReadByte();
                if ((size1 & 0x80) != 0)
                {
                    if ((size1 & 0x7F) != 0)
                    {
                        size1 &= 0x7F;
                        byte data = reader.ReadByte();
                        for (int i = 0; i < size1; i++)
                        {
                            buf[index + i] = data;
                        }
                        index += size1;
                    }
                    else
                    {
                        var _size2 = reader.ReadBytes(3);
                        int size2 = _size2[0] + (_size2[1] << 8) + (_size2[2] << 16);
                        byte data = reader.ReadByte();
                        for (int i = 0; i < size2; i++)
                        {
                            buf[index + i] = data;
                        }
                        index += size2;
                    }
                }
                else
                {
                    if (size1 != 0)
                    {
                        for (int i = 0; i < size1; i++)
                        {
                            buf[index + i] = reader.ReadByte();
                        }
                        index += size1;
                    }
                    else
                    {
                        var _size2 = reader.ReadBytes(3);
                        int size2 = _size2[0] + (_size2[1] << 8) + (_size2[2] << 16);
                        for (int i = 0; i < size2; i++)
                        {
                            buf[index + i] = reader.ReadByte();
                        }
                        index += size2;
                    }
                }
            }
        }

        public static void CompressUInt32Array(BinaryWriter writer, uint[] array)
        {
            var buf = new byte[array.Length * sizeof(uint)];
            Buffer.BlockCopy(array, 0, buf, 0, buf.Length);
            Comp(writer, buf);
        }

        static bool IsSame4Bytes(ReadOnlySpan<byte> x)
        {
            return x[0] == x[1] && x[0] == x[2] && x[0] == x[3];
        }

        public static void Comp(BinaryWriter writer, byte[] data)
        {
            var span = new ReadOnlySpan<byte>(data);
            while (span.Length > 0)
            {
                if (span.Length >= 4 && IsSame4Bytes(span))
                {
                    byte b = span[0];
                    int rep = 4;
                    while (rep < span.Length)
                    {
                        if (b == span[rep])
                        {
                            rep++;
                        }
                        else
                        {
                            break;
                        }
                        if (rep >= 0x7F_FFFF)
                        {
                            break;
                        }
                    }
                    if (rep < 0x80)
                    {
                        byte size = (byte)(rep | 0x80);
                        writer.Write(size);
                    }
                    else
                    {
                        writer.Write((byte)0x80);
                        var size = BitConverter.GetBytes(rep).Take(3).ToArray();
                        writer.Write(size);
                    }
                    writer.Write(b);
                    span = span.Slice(rep);
                }
                else
                {
                    int rep = 4;
                    while (rep < span.Length)
                    {
                        if (!IsSame4Bytes(span.Slice(rep - 3)))
                        {
                            rep++;
                        }
                        else
                        {
                            rep -= 3;
                            break;
                        }
                        if (rep >= 0x7F_FFFF)
                        {
                            break;
                        }
                    }
                    if (rep < 0x80)
                    {
                        writer.Write((byte)rep);
                    }
                    else
                    {
                        writer.Write((byte)0x00);
                        var size = BitConverter.GetBytes(rep).Take(3).ToArray();
                        writer.Write(size);
                    }
                    writer.Write(span.Slice(0, rep).ToArray());
                    span = span.Slice(rep);
                }
            }
        }
    }
}
