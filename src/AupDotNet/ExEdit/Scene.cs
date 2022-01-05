using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit
{
    /// <summary>
    /// シーン情報フラグ
    /// </summary>
    [Flags]
    public enum SceneFlag
    {
        /// <summary>
        /// おそらく常に有効なフラグ
        /// </summary>
        Base = 1,

        /// <summary>
        /// アルファチャンネルあり
        /// </summary>
        Alpha = 2,
    }

    /// <summary>
    /// 拡張編集のシーン情報を表すクラス。
    /// </summary>
    public class Scene
    {
        /// <summary>
        /// シーン情報のバイナリサイズ。
        /// </summary>
        public static readonly int Size = 220;

        /// <summary>
        /// シーン名の最大バイト数。
        /// </summary>
        public static readonly int MaxNameLength = 64;

        /// <summary>
        /// シーン番号
        /// </summary>
        public uint SceneIndex { get; set; }

        /// <summary>
        /// シーンのフラグ
        /// </summary>
        public SceneFlag Flag { get; set; }

        private string _name = string.Empty;
        /// <summary>
        /// シーン名
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (value.GetSjisByteCount() >= MaxNameLength)
                {
                    throw new MaxByteCountOfStringException(nameof(Name), MaxNameLength);
                }
                _name = value;
            }
        }

        /// <summary>
        /// 横幅。
        /// </summary>
        public uint Width { get; set; }

        /// <summary>
        /// 高さ。
        /// </summary>
        public uint Height { get; set; }

        /// <summary>
        /// フレーム数。
        /// </summary>
        public uint MaxFrame { get; set; }

        /// <summary>
        /// カーソル位置。
        /// </summary>
        public uint Cursor { get; set; }

        /// <summary>
        /// タイムラインのズームレベル。
        /// </summary>
        public uint Zoom { get; set; }

        /// <summary>
        /// タイムラインの時間軸方向のスクロール位置。
        /// </summary>
        public uint TimeScroll { get; set; }

        /// <summary>
        /// 選択中のオブジェクトのインデックス。
        /// </summary>
        public uint EditingObject { get; set; }

        /// <summary>
        /// 選択中フレームの開始フレーム。
        /// </summary>
        public uint SelectedFrameStart { get; set; }

        /// <summary>
        /// 選択中フレームの終了フレーム。
        /// </summary>
        public uint SelectedFrameEnd { get; set; }

        /// <summary>
        /// グリッド(BPM)の表示
        /// </summary>
        public bool EnableBpmGrid { get; set; }

        /// <summary>
        /// グリッド(BPM)のテンポ
        /// </summary>
        public uint BpmGridTempo { get; set; }

        /// <summary>
        /// グリッド(BPM)の基準フレーム番号
        /// </summary>
        public uint BpmGridOffset { get; set; }

        /// <summary>
        /// グリッド(XY軸)の表示
        /// </summary>
        public bool EnableXYGrid { get; set; }

        /// <summary>
        /// グリッド(XY軸)の横幅
        /// </summary>
        public uint XYGridWidth { get; set; }

        /// <summary>
        /// グリッド(XY軸)の縦幅
        /// </summary>
        public uint XYGridHeight { get; set; }

        /// <summary>
        /// グリッド(カメラ制御)の表示
        /// </summary>
        public bool EnableCameraGrid { get; set; }

        /// <summary>
        /// グリッド(カメラ制御)の幅
        /// </summary>
        public uint CameraGridSize { get; set; }

        /// <summary>
        /// グリッド(カメラ制御)の数量
        /// </summary>
        public uint CameraGridNum { get; set; }

        /// <summary>
        /// フレーム領域外の表示
        /// </summary>
        public bool ShowOutsideFrame { get; set; }

        /// <summary>
        /// フレーム領域外の大きさ
        /// </summary>
        public uint OutsideFrameScale { get; set; }

        /// <summary>
        /// グリッド(BPM)の拍
        /// </summary>
        public uint BpmGridBeat { get; set; }
        
        /// <summary>
        /// タイムラインのレイヤー方向のスクロール位置。
        /// </summary>
        public uint LayerScroll { get; set; }

        /// <summary>
        /// オフセットアドレス 0xA0 から 0xDC
        /// </summary>
        public byte[] Field0xA0_0xDC { get; } = new byte[60];

        /// <summary>
        /// <see cref="Scene"/> のインスタンスを初期化します。
        /// </summary>
        public Scene()
        {
        }

        /// <summary>
        /// <see cref="Scene"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="data">シーン情報</param>
        /// <exception cref="ArgumentException"><c>data</c> の長さが正しくありません。</exception>
        public Scene(ReadOnlySpan<byte> data)
        {
            if (data.Length < Size)
            {
                throw new ArgumentException($"data length requires {Size} bytes.");
            }
            SceneIndex = data.Slice(0, 4).ToUInt32();
            Flag = (SceneFlag)data.Slice(4, 4).ToUInt32();
            Name = data.Slice(8, 64).ToCleanSjisString();
            Width = data.Slice(0x48, 4).ToUInt32();
            Height = data.Slice(0x4C, 4).ToUInt32();
            MaxFrame = data.Slice(0x50, 4).ToUInt32();
            Cursor = data.Slice(0x54, 4).ToUInt32();
            Zoom = data.Slice(0x58, 4).ToUInt32();
            TimeScroll = data.Slice(0x5C, 4).ToUInt32();
            EditingObject = data.Slice(0x60, 4).ToUInt32();
            SelectedFrameStart = data.Slice(0x64, 4).ToUInt32();
            SelectedFrameEnd = data.Slice(0x68, 4).ToUInt32();
            EnableBpmGrid = data.Slice(0x6C, 4).ToBool();
            BpmGridTempo = data.Slice(0x70, 4).ToUInt32();
            BpmGridOffset = data.Slice(0x74, 4).ToUInt32();
            EnableXYGrid = data.Slice(0x78, 4).ToBool();
            XYGridWidth = data.Slice(0x7C, 4).ToUInt32();
            XYGridHeight = data.Slice(0x80, 4).ToUInt32();
            EnableCameraGrid = data.Slice(0x84, 4).ToBool();
            CameraGridSize = data.Slice(0x88, 4).ToUInt32();
            CameraGridNum = data.Slice(0x8C, 4).ToUInt32();
            ShowOutsideFrame = data.Slice(0x90, 4).ToBool();
            OutsideFrameScale = data.Slice(0x94, 4).ToUInt32();
            BpmGridBeat = data.Slice(0x98, 4).ToUInt32();
            LayerScroll = data.Slice(0x9C, 4).ToUInt32();
            data.Slice(0xA0, Field0xA0_0xDC.Length).CopyTo(Field0xA0_0xDC);
        }

        /// <summary>
        /// シーン情報をダンプします。
        /// </summary>
        /// <param name="data">シーン情報を格納する配列</param>
        public void Dump(Span<byte> data)
        {
            SceneIndex.ToBytes().CopyTo(data);
            ((uint)Flag).ToBytes().CopyTo(data.Slice(4));
            Name.ToSjisBytes(MaxNameLength).CopyTo(data.Slice(8, MaxNameLength));
            Width.ToBytes().CopyTo(data.Slice(0x48));
            Height.ToBytes().CopyTo(data.Slice(0x4C));
            MaxFrame.ToBytes().CopyTo(data.Slice(0x50));
            Cursor.ToBytes().CopyTo(data.Slice(0x54));
            Zoom.ToBytes().CopyTo(data.Slice(0x58));
            TimeScroll.ToBytes().CopyTo(data.Slice(0x5C));
            EditingObject.ToBytes().CopyTo(data.Slice(0x60));
            SelectedFrameStart.ToBytes().CopyTo(data.Slice(0x64));
            SelectedFrameEnd.ToBytes().CopyTo(data.Slice(0x68));
            EnableBpmGrid.ToBytes().CopyTo(data.Slice(0x6C));
            BpmGridTempo.ToBytes().CopyTo(data.Slice(0x70));
            BpmGridOffset.ToBytes().CopyTo(data.Slice(0x74));
            EnableXYGrid.ToBytes().CopyTo(data.Slice(0x78));
            XYGridWidth.ToBytes().CopyTo(data.Slice(0x7C));
            XYGridHeight.ToBytes().CopyTo(data.Slice(0x80));
            EnableCameraGrid.ToBytes().CopyTo(data.Slice(0x84));
            CameraGridSize.ToBytes().CopyTo(data.Slice(0x88));
            CameraGridNum.ToBytes().CopyTo(data.Slice(0x8C));
            ShowOutsideFrame.ToBytes().CopyTo(data.Slice(0x90));
            OutsideFrameScale.ToBytes().CopyTo(data.Slice(0x94));
            BpmGridBeat.ToBytes().CopyTo(data.Slice(0x98));
            LayerScroll.ToBytes().CopyTo(data.Slice(0x9C));
            Field0xA0_0xDC.CopyTo(data.Slice(0xA0));
        }
    }
}
