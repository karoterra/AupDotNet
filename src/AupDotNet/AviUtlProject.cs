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

        public uint HandleSize { get; set; }
        public uint Flag { get; set; }
        public string EditFilename { get; set; }
        public string OutputFilename { get; set; }
        public string ProjectFilename { get; set; }
        public byte[] EditHandleData { get; set; }
        public int FrameNum { get; set; }

        public uint[] Video { get; set; }
        public uint[] Audio { get; set; }
        public uint[] Array2 { get; set; }
        public uint[] Array3 { get; set; }
        public byte[] Inter { get; set; }
        public byte[] Index24Fps { get; set; }
        public byte[] EditFlag { get; set; }
        public byte[] Config { get; set; }
        public byte[] Array8 { get; set; }
        public byte[] Vcm { get; set; }

        public byte[] DataBeforeFooter { get; set; }

        public List<FilterProject> FilterProjects { get; set; }

        public void Read(BinaryReader reader)
        {
            var baseStream = reader.BaseStream;

            var header = reader.ReadBytes(32).ToSjisString();
            if (header != Header)
            {
                throw new FileFormatException("Cannot find AviUtl ProjectFile header.");
            }

            HandleSize = reader.ReadUInt32();
            Flag = reader.ReadUInt32();
            EditFilename = reader.ReadBytes(260).ToSjisString().CutNull();
            OutputFilename = reader.ReadBytes(260).ToSjisString().CutNull();
            var buf = new byte[HandleSize - 260 * 2 - 4];
            Decomp(reader, buf);
            ProjectFilename = new ReadOnlySpan<byte>(buf, 0, 260).ToSjisString().CutNull();
            EditHandleData = buf.Skip(260).ToArray();
            FrameNum = reader.ReadInt32();

            Video = ReadCompressedUInt32Array(reader, FrameNum);
            Audio = ReadCompressedUInt32Array(reader, FrameNum);
            Array2 = ReadCompressedUInt32Array(reader, FrameNum);
            Array3 = ReadCompressedUInt32Array(reader, FrameNum);
            Inter = ReadCompressedUInt8Array(reader, FrameNum);
            Index24Fps = ReadCompressedUInt8Array(reader, FrameNum);
            EditFlag = ReadCompressedUInt8Array(reader, FrameNum);
            Config = ReadCompressedUInt8Array(reader, FrameNum);
            Array8 = ReadCompressedUInt8Array(reader, FrameNum);
            Vcm = ReadCompressedUInt8Array(reader, FrameNum);

            DataBeforeFooter = SkipToFooter(reader);

            FilterProjects = new List<FilterProject>();
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
            writer.Write(EditFilename.ToSjisBytes(260));
            writer.Write(OutputFilename.ToSjisBytes(260));
            Comp(writer, ProjectFilename.ToSjisBytes(260));
            Comp(writer, EditHandleData);
            writer.Write(FrameNum);
            foreach(var array in new uint[][]{ Video, Audio, Array2, Array3 })
            {
                var buf = new byte[array.Length * sizeof(uint)];
                Buffer.BlockCopy(array, 0, buf, 0, buf.Length);
                Comp(writer, buf);
            }
            Comp(writer, Inter);
            Comp(writer, Index24Fps);
            Comp(writer, EditFlag);
            Comp(writer, Config);
            Comp(writer, Array8);
            Comp(writer, Vcm);
            writer.Write(DataBeforeFooter);
            writer.Write(Header.ToSjisBytes());

            foreach(var filter in FilterProjects)
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
