using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit
{
    public class ExEditProject : FilterProject
    {
        public static readonly uint NoEditingObject = 0xFFFF_FFFF;
        protected static readonly double BpmGridTempoScale = 10000.0;

        public uint Field0xC { get; set; }
        public uint Zoom { get; set; }
        public uint Field0x14 { get; set; }
        public uint EditingObject { get; set; }
        public uint Field0x1C { get; set; }
        public uint Field0x20 { get; set; }
        public uint Field0x24 { get; set; }
        public uint Field0x28 { get; set; }
        public uint Version { get; set; }
        public bool EnableBpmGrid { get; set; }
        public double BpmGridTempo { get; set; }
        public uint BpmGridOffset { get; set; }
        public bool EnableXYGrid { get; set; }
        public uint XYGridWidth { get; set; }
        public uint XYGridHeight { get; set; }
        public bool EnableCameraGrid { get; set; }
        public uint CameraGridWidth { get; set; }
        public uint CameraGridHeight { get; set; }
        public bool ShowOutsideFrame { get; set; }
        public uint OutsideFrameScale { get; set; }
        public uint BpmGridBeat { get; set; }
        public uint Field0x60 { get; set; }
        public uint EditingScene { get; set; }
        public uint Field0x78 { get; set; }

        public readonly byte[] Field0x80_0xFF = new byte[128];

        public readonly List<Layer> Layers;
        public readonly List<Scene> Scenes;
        public TrackbarScript[] TrackbarScripts { get; set; }

        private List<EffectType> _effectTypes;
        public readonly ReadOnlyCollection<EffectType> EffectTypes;

        public readonly List<TimelineObject> Objects;

        public ExEditProject()
        {
            Layers = new List<Layer>();
            Scenes = new List<Scene>();
            _effectTypes = EffectType.Defaults.ToList();
            EffectTypes = new ReadOnlyCollection<EffectType>(_effectTypes);
            Objects = new List<TimelineObject>();
        }

        public ExEditProject(RawFilterProject rawFilter)
        {
            Name = "拡張編集";

            var data = new ReadOnlySpan<byte>(rawFilter.Data);
            var effectTypeNum = data.Slice(4, 4).ToUInt32();
            var objectNum = data.Slice(8, 4).ToUInt32();
            Field0xC = data.Slice(0xC, 4).ToUInt32();
            Zoom = data.Slice(0x10, 4).ToUInt32();
            Field0x14 = data.Slice(0x14, 4).ToUInt32();
            EditingObject = data.Slice(0x18, 4).ToUInt32();
            Field0x1C = data.Slice(0x1C, 4).ToUInt32();
            Field0x20 = data.Slice(0x20, 4).ToUInt32();
            Field0x24 = data.Slice(0x24, 4).ToUInt32();
            Field0x28 = data.Slice(0x28, 4).ToUInt32();
            Version = data.Slice(0x2C, 4).ToUInt32();
            EnableBpmGrid = data.Slice(0x30, 4).ToInt32() != 0;
            BpmGridTempo = data.Slice(0x34, 4).ToUInt32() / BpmGridTempoScale;
            BpmGridOffset = data.Slice(0x38, 4).ToUInt32();
            EnableXYGrid = data.Slice(0x3C, 4).ToInt32() != 0;
            XYGridWidth = data.Slice(0x40, 4).ToUInt32();
            XYGridHeight = data.Slice(0x44, 4).ToUInt32();
            EnableCameraGrid = data.Slice(0x48, 4).ToInt32() != 0;
            CameraGridWidth = data.Slice(0x4C, 4).ToUInt32();
            CameraGridHeight = data.Slice(0x50, 4).ToUInt32();
            ShowOutsideFrame = data.Slice(0x54, 4).ToInt32() != 0;
            OutsideFrameScale = data.Slice(0x58, 4).ToUInt32();
            BpmGridBeat = data.Slice(0x5C, 4).ToUInt32();
            Field0x60 = data.Slice(0x60, 4).ToUInt32();
            EditingScene = data.Slice(0x64, 4).ToUInt32();
            var sceneNum = data.Slice(0x68, 4).ToUInt32();
            var layerNum = data.Slice(0x6C, 4).ToUInt32();
            var trackbarScriptNum = data.Slice(0x7C, 4).ToUInt32();
            Field0x78 = data.Slice(0x78, 4).ToUInt32();
            data.Slice(0x80, Field0x80_0xFF.Length).CopyTo(Field0x80_0xFF);

            int cursor = 0x100;
            Layers = new List<Layer>((int)layerNum);
            for (uint i = 0; i < layerNum; i++)
            {
                Layers.Add(new Layer(data.Slice(cursor, Layer.Size)));
                cursor += Layer.Size;
            }
            Scenes = new List<Scene>((int)sceneNum);
            for (uint i = 0; i < sceneNum; i++)
            {
                Scenes.Add(new Scene(data.Slice(cursor, Scene.Size)));
                cursor += Scene.Size;
            }
            TrackbarScripts = new TrackbarScript[trackbarScriptNum];
            for (uint i = 0; i < TrackbarScripts.Length; i++)
            {
                TrackbarScripts[i] = new TrackbarScript(data.Slice(cursor, TrackbarScript.Size));
                cursor += TrackbarScript.Size;
            }
            _effectTypes = new List<EffectType>();
            EffectTypes = new ReadOnlyCollection<EffectType>(_effectTypes);
            for (int i = 0; i < effectTypeNum; i++)
            {
                _effectTypes.Add(new EffectType(data.Slice(cursor, EffectType.Size), i));
                cursor += EffectType.Size;
            }
            Objects = new List<TimelineObject>((int)objectNum);
            for (int i = 0; i < objectNum; i++)
            {
                Objects.Add(new TimelineObject(data.Slice(cursor), EffectTypes));
                cursor += (int)Objects[i].Size;
            }
        }

        public override byte[] DumpData()
        {
            int size = 0x100 + Layer.Size * Layers.Count + Scene.Size * Scenes.Count;
            size += TrackbarScript.Size * TrackbarScripts.Length + EffectType.Size * EffectTypes.Count;
            size += Objects.Sum(obj => (int)obj.Size);
            var data = new byte[size];

            "80EE".ToSjisBytes().CopyTo(data, 0);
            EffectTypes.Count.ToBytes().CopyTo(data, 4);
            Objects.Count.ToBytes().CopyTo(data, 8);
            Field0xC.ToBytes().CopyTo(data, 0xC);
            Zoom.ToBytes().CopyTo(data, 0x10);
            Field0x14.ToBytes().CopyTo(data, 0x14);
            EditingObject.ToBytes().CopyTo(data, 0x18);
            Field0x1C.ToBytes().CopyTo(data, 0x1C);
            Field0x20.ToBytes().CopyTo(data, 0x20);
            Field0x24.ToBytes().CopyTo(data, 0x24);
            Field0x28.ToBytes().CopyTo(data, 0x28);
            Version.ToBytes().CopyTo(data, 0x2C);
            (EnableBpmGrid ? 1 : 0).ToBytes().CopyTo(data, 0x30);
            ((uint)(BpmGridTempo * BpmGridTempoScale)).ToBytes().CopyTo(data, 0x34);
            BpmGridOffset.ToBytes().CopyTo(data, 0x38);
            (EnableXYGrid ? 1 : 0).ToBytes().CopyTo(data, 0x3C);
            XYGridWidth.ToBytes().CopyTo(data, 0x40);
            XYGridHeight.ToBytes().CopyTo(data, 0x44);
            (EnableCameraGrid ? 1 : 0).ToBytes().CopyTo(data, 0x48);
            CameraGridWidth.ToBytes().CopyTo(data, 0x4C);
            CameraGridHeight.ToBytes().CopyTo(data, 0x50);
            (ShowOutsideFrame ? 1 : 0).ToBytes().CopyTo(data, 0x54);
            OutsideFrameScale.ToBytes().CopyTo(data, 0x58);
            BpmGridBeat.ToBytes().CopyTo(data, 0x5C);
            Field0x60.ToBytes().CopyTo(data, 0x60);
            EditingScene.ToBytes().CopyTo(data, 0x64);
            Scenes.Count.ToBytes().CopyTo(data, 0x68);
            Layers.Count.ToBytes().CopyTo(data, 0x6C);
            Scene.Size.ToBytes().CopyTo(data, 0x70);
            Layer.Size.ToBytes().CopyTo(data, 0x74);
            Field0x78.ToBytes().CopyTo(data, 0x78);
            TrackbarScripts.Length.ToBytes().CopyTo(data, 0x7C);
            Field0x80_0xFF.CopyTo(data, 0x80);

            int cursor = 0x100;
            foreach (var layer in Layers)
            {
                layer.Dump(new Span<byte>(data, cursor, Layer.Size));
                cursor += Layer.Size;
            }
            foreach (var scene in Scenes)
            {
                scene.Dump(new Span<byte>(data, cursor, Scene.Size));
                cursor += Scene.Size;
            }
            foreach (var ts in TrackbarScripts)
            {
                ts.Dump(new Span<byte>(data, cursor, TrackbarScript.Size));
                cursor += TrackbarScript.Size;
            }
            foreach (var ft in EffectTypes)
            {
                ft.Dump(new Span<byte>(data, cursor, EffectType.Size));
                cursor += EffectType.Size;
            }
            foreach (var obj in Objects)
            {
                obj.Dump(new Span<byte>(data, cursor, (int)obj.Size), EditingScene);
                cursor += (int)obj.Size;
            }

            return data;
        }

        public void SortObjects()
        {
            Objects.Sort((x, y) =>
            {
                int diff = (int)x.SceneIndex - (int)y.SceneIndex;
                if (diff != 0)
                {
                    if (x.SceneIndex == EditingScene) return -1;
                    else if (y.SceneIndex == EditingScene) return 1;
                    return diff;
                }
                diff = (int)x.LayerIndex - (int)y.LayerIndex;
                if (diff != 0) return diff;
                diff = (int)x.StartFrame - (int)y.StartFrame;
                return diff;
            });
        }
    }
}
