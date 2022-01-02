using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit
{
    /// <summary>
    /// レイヤー情報フラグ
    /// </summary>
    [Flags]
    public enum LayerFlag
    {
        /// <summary>
        /// レイヤーの非表示
        /// </summary>
        Hide = 1,

        /// <summary>
        /// レイヤーのロック
        /// </summary>
        Lock = 2,

        /// <summary>
        /// 座標のリンク
        /// </summary>
        Link = 0x10,

        /// <summary>
        /// 上のオブジェクトでクリッピング
        /// </summary>
        Clipping = 0x20,
    }

    /// <summary>
    /// 拡張編集のレイヤー情報を表すクラス。
    /// </summary>
    public class Layer
    {
        /// <summary>
        /// レイヤー情報のバイナリサイズ。
        /// </summary>
        public static readonly int Size = 76;

        /// <summary>
        /// レイヤー名の最大バイト数。
        /// </summary>
        public static readonly int MaxNameLength = 64;

        /// <summary>
        /// シーン番号
        /// </summary>
        public uint SceneIndex { get; set; }

        /// <summary>
        /// レイヤー番号
        /// </summary>
        public uint LayerIndex { get; set; }

        /// <summary>
        /// レイヤーのフラグ
        /// </summary>
        public LayerFlag Flag { get; set; }

        private string _name;
        /// <summary>
        /// レイヤー名
        /// </summary>
        public string Name {
            get => _name;
            set
            {
                if (value.GetSjisByteCount() >= MaxNameLength)
                {
                    throw new ArgumentException($"Byte count of {nameof(Name)} must be less than {MaxNameLength}.");
                }
                _name = value;
            }
        }

        /// <summary>
        /// <see cref="Layer"/> のインスタンスを初期化します。
        /// </summary>
        public Layer()
        {
        }

        /// <summary>
        /// <see cref="Layer"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="data">レイヤー情報</param>
        /// <exception cref="ArgumentException"><c>data</c> の長さが正しくありません。</exception>
        public Layer(ReadOnlySpan<byte> data)
        {
            if (data.Length < Size)
            {
                throw new ArgumentException($"data length requires {Size} bytes.");
            }
            SceneIndex = data.Slice(0, 4).ToUInt32();
            LayerIndex = data.Slice(4, 4).ToUInt32();
            Flag = (LayerFlag)data.Slice(8, 4).ToUInt32();
            Name = data.Slice(12).ToCleanSjisString();
        }

        /// <summary>
        /// レイヤー情報をダンプします。
        /// </summary>
        /// <param name="data">レイヤー情報を格納する配列</param>
        public void Dump(Span<byte> data)
        {
            SceneIndex.ToBytes().CopyTo(data);
            LayerIndex.ToBytes().CopyTo(data.Slice(4));
            ((uint)Flag).ToBytes().CopyTo(data.Slice(8));
            Name.ToSjisBytes(MaxNameLength).CopyTo(data.Slice(12, MaxNameLength));
        }
    }
}
