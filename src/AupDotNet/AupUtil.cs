using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet
{
    public static class AupUtil
    {
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

        public static byte[] DecompressUInt8Array(BinaryReader reader, int length)
        {
            var buf = new byte[length];
            Decomp(reader, buf);
            return buf;
        }

        public static uint[] DecompressUInt32Array(BinaryReader reader, int length)
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

        public static void CompressUInt32Array(BinaryWriter writer, uint[] array)
        {
            var buf = new byte[array.Length * sizeof(uint)];
            Buffer.BlockCopy(array, 0, buf, 0, buf.Length);
            Comp(writer, buf);
        }
    }
}
