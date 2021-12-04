using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit
{
    [Flags]
    public enum SceneFlag
    {
        Base = 1,
        Alpha = 2,
    }

    public class Scene
    {
        public static readonly int Size = 220;
        public static readonly int MaxNameLength = 64;

        public uint SceneIndex { get; set; }
        public SceneFlag Flag { get; set; }

        private string _name;
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

        public uint Width { get; set; }
        public uint Height { get; set; }
        public uint MaxFrame { get; set; }
        public uint Cursor { get; set; }
        public uint Zoom { get; set; }
        public uint TimeScroll { get; set; }
        public uint EditingObject { get; set; }
        public uint SelectedFrameStart { get; set; }
        public uint SelectedFrameEnd { get; set; }
        public bool EnableBpmGrid { get; set; }
        public uint BpmGridTempo { get; set; }
        public uint BpmGridOffset { get; set; }
        public bool EnableXYGrid { get; set; }
        public uint XYGridWidth { get; set; }
        public uint XYGridHeight { get; set; }
        public bool EnableCameraGrid { get; set; }
        public uint CameraGridSize { get; set; }
        public uint CameraGridNum { get; set; }
        public bool ShowOutsideFrame { get; set; }
        public uint OutsideFrameScale { get; set; }
        public uint BpmGridBeat { get; set; }
        public uint LayerScroll { get; set; }

        public byte[] Field0xA0_0xDC { get; } = new byte[60];

        public Scene()
        {
        }

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
