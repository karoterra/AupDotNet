using System;

namespace Karoterra.AupDotNet
{
    /// <summary>
    /// フレームのインターレース。
    /// </summary>
    public enum FrameStatusInter : byte
    {
        /// <summary>標準</summary>
        Normal = 0,
        /// <summary>反転</summary>
        Reverse,
        /// <summary>奇数</summary>
        Odd,
        /// <summary>偶数</summary>
        Even,
        /// <summary>二重化</summary>
        Mix,
        /// <summary>自動</summary>
        Auto,
    }

    /// <summary>
    /// 編集フラグ。
    /// </summary>
    [Flags]
    public enum EditFrameEditFlag : byte
    {
        /// <summary>キーフレーム</summary>
        Keyframe = 1,
        /// <summary>マークフレーム</summary>
        MarkFrame = 2,
        /// <summary>優先間引きフレーム</summary>
        DelFrame = 4,
        /// <summary>コピーフレーム</summary>
        NullFrame = 8,
    }

    /// <summary>
    /// フレームの情報を表すクラス。
    /// </summary>
    public class FrameStatus
    {
        /// <summary>
        /// 実際の映像データ番号。
        /// </summary>
        public uint Video { get; set; }

        /// <summary>
        /// 実際の音声データ番号。
        /// </summary>
        public uint Audio { get; set; }

        /// <summary>
        /// フレーム情報2。
        /// </summary>
        public uint Field2 { get; set; }

        /// <summary>
        /// フレーム情報3。
        /// </summary>
        public uint Field3 { get; set; }

        /// <summary>
        /// フレームのインターレース。
        /// </summary>
        public FrameStatusInter Inter { get; set; }

        /// <summary>
        /// Index 24 fps.
        /// </summary>
        /// <remarks>
        /// 現在は使用されていません。
        /// (「フィルタプラグイン ヘッダーファイル for AviUtl version 0.99k 以降」より)
        /// </remarks>
        public byte Index24Fps { get; set; }

        /// <summary>
        /// 編集フラグ。
        /// </summary>
        public EditFrameEditFlag EditFlag { get; set; }

        /// <summary>
        /// フレームのプロファイル環境の番号。
        /// </summary>
        public byte Config { get; set; }

        /// <summary>
        /// フレームの圧縮設定の番号。
        /// </summary>
        public byte Vcm { get; set; }

        /// <summary>
        /// クリップボードから貼り付けた画像の番号。
        /// </summary>
        public byte Clip { get; set; }

        /// <summary>
        /// <see cref="FrameStatus"/> のインスタンスを初期化します。
        /// </summary>
        public FrameStatus()
        {
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            return obj is FrameStatus frame &&
                Video == frame.Video && Audio == frame.Audio &&
                Field2 == frame.Field2 && Field3 == frame.Field3 &&
                Inter == frame.Inter && Index24Fps == frame.Index24Fps &&
                EditFlag == frame.EditFlag && Config == frame.Config &&
                Vcm == frame.Vcm && Clip == frame.Clip;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return (int)Video ^ (int)Audio ^ (int)Field2 ^ (int)Field3 ^
                (int)Inter ^ (Index24Fps << 8) ^ ((int)EditFlag << 16) ^ (Config << 24) ^
                Vcm ^ (Clip << 8);
        }
    }
}
