using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit
{
    /// <summary>
    /// フィルタ効果の定義を表すクラス。
    /// </summary>
    public partial class EffectType
    {
        /// <summary>
        /// フィルタ効果定義のバイナリサイズ。
        /// </summary>
        public static readonly int Size = 112;

        /// <summary>
        /// フィルタ効果名の最大バイト数。
        /// </summary>
        public static readonly int MaxNameLength = 96;

        /// <summary>
        /// フィルタ効果のID。
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// フィルタ効果の定義フラグ
        /// </summary>
        public uint Flag { get; }

        /// <summary>
        /// トラックバーの個数。
        /// </summary>
        public uint TrackbarNum { get; }

        /// <summary>
        /// チェックボックスの個数。
        /// </summary>
        public uint CheckboxNum { get; }

        /// <summary>
        /// 拡張データのバイト長。
        /// </summary>
        public uint ExtSize { get; }

        /// <summary>
        /// フィルタ効果の名前。
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 各トラックバーの定義。
        /// </summary>
        public TrackbarDefinition[] Trackbars { get; }

        /// <summary>
        /// 各チェックボックスの定義。
        /// </summary>
        public CheckboxDefinition[] Checkboxes { get; }

        /// <summary>
        /// <see cref="EffectType"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="flag">フラグ</param>
        /// <param name="trackbarNum">トラックバーの個数</param>
        /// <param name="checkboxNum">チェックボックスの個数</param>
        /// <param name="extSize">拡張データのバイト長</param>
        /// <param name="name">名前</param>
        public EffectType(int id, uint flag, uint trackbarNum, uint checkboxNum, uint extSize, string name)
        {
            Id = id;
            Flag = flag;
            TrackbarNum = trackbarNum;
            CheckboxNum = checkboxNum;
            ExtSize = extSize;
            Name = name;

            Trackbars = new TrackbarDefinition[TrackbarNum];
            Checkboxes = new CheckboxDefinition[CheckboxNum];
        }

        /// <summary>
        /// <see cref="EffectType"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="data">定義データ</param>
        /// <param name="id">ID</param>
        /// <exception cref="ArgumentException"><c>data</c> の長さが正しくありません。</exception>
        public EffectType(ReadOnlySpan<byte> data, int id)
        {
            if (data.Length < Size)
            {
                throw new ArgumentException($"data length requires {Size} bytes.");
            }
            Id = id;
            Flag = data.Slice(0, 4).ToUInt32();
            TrackbarNum = data.Slice(4, 4).ToUInt32();
            CheckboxNum = data.Slice(8, 4).ToUInt32();
            ExtSize = data.Slice(12, 4).ToUInt32();
            Name = data.Slice(16).ToCleanSjisString();

            Trackbars = new TrackbarDefinition[TrackbarNum];
            Checkboxes = new CheckboxDefinition[CheckboxNum];
        }

        /// <summary>
        /// <see cref="EffectType"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="flag">フラグ</param>
        /// <param name="trackbarNum">トラックバーの個数</param>
        /// <param name="checkboxNum">チェックボックスの個数</param>
        /// <param name="extSize">拡張データのバイト長</param>
        /// <param name="name">名前</param>
        /// <param name="trackbars">トラックバーの定義</param>
        /// <param name="checkboxes">チェックボックスの定義</param>
        public EffectType(
            int id, uint flag, uint trackbarNum, uint checkboxNum, uint extSize, string name,
            TrackbarDefinition[] trackbars, CheckboxDefinition[] checkboxes)
            : this(id, flag, trackbarNum, checkboxNum, extSize, name)
        {
            trackbars.CopyTo(Trackbars, 0);
            checkboxes.CopyTo(Checkboxes, 0);
        }

        /// <summary>
        /// <see cref="EffectType"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="data">定義データ</param>
        /// <param name="id">ID</param>
        /// <param name="trackbars">トラックバーの定義</param>
        /// <param name="checkboxes">チェックボックスの定義</param>
        /// <exception cref="ArgumentException"><c>data</c> の長さが正しくありません。</exception>
        public EffectType(ReadOnlySpan<byte> data, int id, TrackbarDefinition[] trackbars, CheckboxDefinition[] checkboxes)
            : this(data, id)
        {
            trackbars.CopyTo(Trackbars, 0);
            checkboxes.CopyTo(Checkboxes, 0);
        }

        /// <summary>
        /// フィルタ効果の定義をダンプする。
        /// </summary>
        /// <param name="data">ダンプしたデータを格納する配列</param>
        public void Dump(Span<byte> data)
        {
            Flag.ToBytes().CopyTo(data);
            TrackbarNum.ToBytes().CopyTo(data.Slice(4));
            CheckboxNum.ToBytes().CopyTo(data.Slice(8));
            ExtSize.ToBytes().CopyTo(data.Slice(12));
            Name.ToSjisBytes(MaxNameLength).CopyTo(data.Slice(16, MaxNameLength));
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is EffectType et &&
                Id == et.Id &&
                Flag == et.Flag &&
                TrackbarNum == et.TrackbarNum &&
                CheckboxNum == et.CheckboxNum &&
                ExtSize == et.ExtSize &&
                Name == et.Name;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Id ^ (int)Flag ^ (int)TrackbarNum ^ (int)CheckboxNum ^ (int)ExtSize ^ Name.GetHashCode();
        }
    }
}
