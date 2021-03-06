using System;
using System.IO;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet
{
    /// <summary>
    /// <see cref="Karoterra.AupDotNet"/> で使うユーティリティ。
    /// </summary>
    public static class AupUtil
    {
        /// <summary>
        /// リーダから圧縮されたデータを読み込む。
        /// </summary>
        /// <param name="reader">リーダ</param>
        /// <param name="buf">読み込んだデータ</param>
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

        /// <summary>
        /// ライタにデータを圧縮して書き込む。
        /// </summary>
        /// <param name="writer">ライタ</param>
        /// <param name="data">書き込むデータ</param>
        public static void Comp(BinaryWriter writer, ReadOnlySpan<byte> data)
        {
            while (data.Length > 0)
            {
                if (data.Length >= 4 && IsSame4Bytes(data))
                {
                    byte b = data[0];
                    int rep = 4;
                    while (rep < data.Length && b == data[rep])
                    {
                        rep++;
                        if (rep >= 0x7F_FFFF) break;
                    }
                    if (rep < 0x80)
                    {
                        byte size = (byte)(rep | 0x80);
                        writer.Write(size);
                    }
                    else
                    {
                        int sizeData = (rep << 8) | 0x80;
                        writer.Write(sizeData.ToBytes());
                    }
                    writer.Write(b);
                    data = data.Slice(rep);
                }
                else
                {
                    int rep = 0;
                    while (rep < data.Length && (data.Length - rep < 4 || !IsSame4Bytes(data.Slice(rep))))
                    {
                        rep++;
                        if (rep >= 0x7F_FFFF) break;
                    }
                    if (rep < 0x80)
                    {
                        writer.Write((byte)rep);
                    }
                    else
                    {
                        int sizeData = rep << 8;
                        writer.Write(sizeData.ToBytes());
                    }
#if NET6_0_OR_GREATER
                    writer.Write(data[..rep]);
#else
                    writer.Write(data.Slice(0, rep).ToArray());
#endif
                    data = data.Slice(rep);
                }
            }
        }

        /// <summary>
        /// リーダから圧縮された <c>byte</c> 配列を読み込む。
        /// </summary>
        /// <param name="reader">リーダ</param>
        /// <param name="length">配列の長さ</param>
        /// <returns></returns>
        public static byte[] DecompressUInt8Array(BinaryReader reader, int length)
        {
            var buf = new byte[length];
            Decomp(reader, buf);
            return buf;
        }

        /// <summary>
        /// リーダから圧縮された <c>uint</c> 配列を読み込む。
        /// </summary>
        /// <param name="reader">リーダ</param>
        /// <param name="length">配列の長さ</param>
        /// <returns></returns>
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

        /// <summary>
        /// ライタに <c>uint</c> 配列を圧縮して書き込む。
        /// </summary>
        /// <param name="writer">ライタ</param>
        /// <param name="array">書き込む配列</param>
        public static void CompressUInt32Array(BinaryWriter writer, uint[] array)
        {
            var buf = new byte[array.Length * sizeof(uint)];
            Buffer.BlockCopy(array, 0, buf, 0, buf.Length);
            Comp(writer, buf);
        }
    }
}
