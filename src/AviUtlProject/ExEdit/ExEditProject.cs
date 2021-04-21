using System;
using System.Linq;
using Karoterra.AviUtlProject.Extensions;

namespace Karoterra.AviUtlProject.ExEdit
{
    public class ExEditProject : FilterProject
    {
        public uint Version { get; set; }

        public Layer[] Layers { get; set; }
        public Scene[] Scenes { get; set; }
        public TrackbarScript[] TrackbarScripts { get; set; }
        public FilterType[] FilterTypes { get; set; }
        public TimelineObject[] Objects { get; set; }

        public ExEditProject(RawFilterProject rawFilter)
        {
            var data = new ReadOnlySpan<byte>(rawFilter.Data);
            var filterTypeNum = data.Slice(4, 4).ToUInt32();
            var objectNum = data.Slice(8, 4).ToUInt32();
            Version = data.Slice(0x2C, 4).ToUInt32();
            var sceneNum = data.Slice(0x68, 4).ToUInt32();
            var layerNum = data.Slice(0x6C, 4).ToUInt32();
            var trackbarScriptNum = data.Slice(0x7C, 4).ToUInt32();

            int cursor = 0x100;
            Layers = new Layer[layerNum];
            for (uint i = 0; i < Layers.Length; i++)
            {
                Layers[i] = new Layer(data.Slice(cursor, Layer.Size));
                cursor += Layer.Size;
            }
            Scenes = new Scene[sceneNum];
            for (uint i = 0; i < Scenes.Length; i++)
            {
                Scenes[i] = new Scene(data.Slice(cursor, Scene.Size));
                cursor += Scene.Size;
            }
            TrackbarScripts = new TrackbarScript[trackbarScriptNum];
            for (uint i = 0; i < TrackbarScripts.Length; i++)
            {
                TrackbarScripts[i] = new TrackbarScript(data.Slice(cursor, TrackbarScript.Size));
                cursor += TrackbarScript.Size;
            }
            FilterTypes = new FilterType[filterTypeNum];
            for (int i = 0; i < FilterTypes.Length; i++)
            {
                FilterTypes[i] = new FilterType(data.Slice(cursor, FilterType.Size));
                cursor += FilterType.Size;
            }
            Objects = new TimelineObject[objectNum];
            for (uint i = 0; i < Objects.Length; i++)
            {
                Objects[i] = new TimelineObject(data.Slice(cursor), FilterTypes);
                cursor += (int)Objects[i].Size;
            }
        }

        public override byte[] DumpData()
        {
            int size = 0x100 + Layer.Size * Layers.Length + Scene.Size * Scenes.Length;
            size += TrackbarScript.Size * TrackbarScripts.Length + FilterType.Size * FilterTypes.Length;
            size += Objects.Sum(obj => (int)obj.Size);
            var data = new byte[size];

            "80EE".ToSjisBytes().CopyTo(data, 0);
            FilterTypes.Length.ToBytes().CopyTo(data, 4);
            Objects.Length.ToBytes().CopyTo(data, 8);
            Version.ToBytes().CopyTo(data, 0x2C);
            Scenes.Length.ToBytes().CopyTo(data, 0x68);
            Layers.Length.ToBytes().CopyTo(data, 0x6C);
            Scene.Size.ToBytes().CopyTo(data, 0x70);
            Layer.Size.ToBytes().CopyTo(data, 0x74);
            TrackbarScripts.Length.ToBytes().CopyTo(data, 0x7C);

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
            foreach (var ft in FilterTypes)
            {
                ft.Dump(new Span<byte>(data, cursor, FilterType.Size));
                cursor += FilterType.Size;
            }
            foreach (var obj in Objects)
            {
                obj.Dump(new Span<byte>(data, cursor, (int)obj.Size), FilterTypes);
                cursor += (int)obj.Size;
            }

            return data;
        }

        public void SortObjects()
        {
            Array.Sort(Objects, (x, y) =>
            {
                int diff = (int)x.SceneIndex - (int)y.SceneIndex;
                if (diff != 0) return diff;
                diff = (int)x.LayerIndex - (int)y.LayerIndex;
                if (diff != 0) return diff;
                diff = (int)x.StartFrame - (int)y.StartFrame;
                return diff;
            });
        }
    }
}
