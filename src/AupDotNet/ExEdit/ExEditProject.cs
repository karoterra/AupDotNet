using System;
using System.Collections.Generic;
using System.Linq;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit
{
    /// <summary>
    /// 拡張編集プラグインを表す <see cref="FilterProject"/>。
    /// </summary>
    public class ExEditProject : FilterProject
    {
        /// <summary>
        /// オブジェクトを選択していない場合の <see cref="EditingObject"/> の値。
        /// </summary>
        public static readonly uint NoEditingObject = 0xFFFF_FFFF;

        /// <summary>
        /// <see cref="BpmGridTempo"/> のスケール。
        /// </summary>
        protected static readonly int BpmGridTempoScale = 10000;

        /// <summary>
        /// オフセットアドレス 0xC
        /// </summary>
        public uint Field0xC { get; set; }

        /// <summary>
        /// タイムラインのズームレベル
        /// </summary>
        public uint Zoom { get; set; }

        /// <summary>
        /// オフセットアドレス 0x14
        /// </summary>
        public uint Field0x14 { get; set; }

        /// <summary>
        /// 選択中のオブジェクトのインデックス
        /// </summary>
        /// <remarks>
        /// オブジェクトを選択していない場合は <see cref="NoEditingObject"/> になります。
        /// </remarks>
        public uint EditingObject { get; set; }

        /// <summary>
        /// オフセットアドレス 0x1C
        /// </summary>
        public uint Field0x1C { get; set; }
        /// <summary>
        /// オフセットアドレス 0x20
        /// </summary>
        public uint Field0x20 { get; set; }
        /// <summary>
        /// オフセットアドレス 0x24
        /// </summary>
        public uint Field0x24 { get; set; }
        /// <summary>
        /// オフセットアドレス 0x28
        /// </summary>
        public uint Field0x28 { get; set; }

        /// <summary>
        /// 拡張編集プラグインのバージョン
        /// </summary>
        public uint Version { get; set; }

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
        public uint CameraGridWidth { get; set; }

        /// <summary>
        /// グリッド(カメラ制御)の数量
        /// </summary>
        public uint CameraGridHeight { get; set; }

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
        /// オフセットアドレス 0x60
        /// </summary>
        public uint Field0x60 { get; set; }

        /// <summary>
        /// 編集中のシーン
        /// </summary>
        public uint EditingScene { get; set; }

        /// <summary>
        /// オフセットアドレス 0x78
        /// </summary>
        public uint Field0x78 { get; set; }

        /// <summary>
        /// オフセットアドレス 0x80 から 0xFF
        /// </summary>
        public byte[] Field0x80_0xFF { get; } = new byte[128];

        /// <summary>
        /// レイヤー情報
        /// </summary>
        public List<Layer> Layers { get; }

        /// <summary>
        /// シーン情報
        /// </summary>
        public List<Scene> Scenes { get; }

        /// <summary>
        /// トラックバー変化方法スクリプト
        /// </summary>
        public List<TrackbarScript> TrackbarScripts { get; }

        /// <summary>
        /// フィルタ効果の定義
        /// </summary>
        public List<EffectType> EffectTypes { get; }

        /// <summary>
        /// タイムラインのオブジェクト
        /// </summary>
        public List<TimelineObject> Objects { get; }

        /// <summary>
        /// <see cref="ExEditProject"/> のインスタンスを初期化します。
        /// </summary>
        public ExEditProject()
        {
            Layers = new List<Layer>();
            Scenes = new List<Scene>();
            TrackbarScripts = new List<TrackbarScript>(TrackbarScript.Defaults);
            EffectTypes = new List<EffectType>(EffectType.Defaults);
            Objects = new List<TimelineObject>();
        }

        /// <summary>
        /// <see cref="ExEditProject"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="rawFilter">生のフィルタプラグインデータ</param>
        /// <param name="effectFactory">フィルタ効果ファクトリ</param>
        public ExEditProject(RawFilterProject rawFilter, IEffectFactory effectFactory = null)
        {
            if (effectFactory == null) effectFactory = new EffectFactory();

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
            BpmGridTempo = data.Slice(0x34, 4).ToUInt32();
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
            TrackbarScripts = new List<TrackbarScript>((int)trackbarScriptNum);
            for (uint i = 0; i < trackbarScriptNum; i++)
            {
                TrackbarScripts.Add(new TrackbarScript(data.Slice(cursor, TrackbarScript.Size)));
                cursor += TrackbarScript.Size;
            }
            EffectTypes = new List<EffectType>((int)effectTypeNum);
            for (int i = 0; i < effectTypeNum; i++)
            {
                EffectTypes.Add(new EffectType(data.Slice(cursor, EffectType.Size), i));
                cursor += EffectType.Size;
            }
            Objects = new List<TimelineObject>((int)objectNum);
            var lastChainGroup = TimelineObject.NoChainGroup;
            for (int i = 0; i < objectNum; i++)
            {
                Objects.Add(new TimelineObject(data.Slice(cursor), lastChainGroup, EffectTypes, effectFactory));
                cursor += (int)Objects[i].Size;
                lastChainGroup = Objects[i].ChainGroup;
            }
        }

        /// <inheritdoc/>
        public override byte[] DumpData()
        {
            int size = 0x100 + Layer.Size * Layers.Count + Scene.Size * Scenes.Count;
            size += TrackbarScript.Size * TrackbarScripts.Count + EffectType.Size * EffectTypes.Count;
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
            BpmGridTempo.ToBytes().CopyTo(data, 0x34);
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
            TrackbarScripts.Count.ToBytes().CopyTo(data, 0x7C);
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

        /// <summary>
        /// <see cref="Objects"/> をソートします。
        /// </summary>
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

        /// <summary>
        /// 指定したシーン番号のシーンをオブジェクトファイルとして出力します。
        /// </summary>
        /// <param name="sceneIndex">シーン番号</param>
        /// <param name="editHandle">EditHandle</param>
        /// <returns>オブジェクトファイル</returns>
        public ExeditObjectFile ExportObject(int sceneIndex, EditHandle editHandle)
        {
            var scene = Scenes.Where(s => s.SceneIndex == sceneIndex).FirstOrDefault();
            int width = 0, height = 0, length = 0;
            if (scene != null)
            {
                width = (int)scene.Width;
                height = (int)scene.Height;
                length = (int)scene.MaxFrame;
            }
            if (width == 0)
            {
                width = editHandle.Width;
                height = editHandle.Height;
            }
            if (sceneIndex == EditingScene)
            {
                length = editHandle.FrameNum;
            }

            var exo = new ExeditObjectFile()
            {
                Width = width,
                Height = height,
                Rate = editHandle.VideoRate,
                Scale = editHandle.VideoScale,
                Length = length,
                AudioRate = editHandle.AudioRate,
                AudioCh = editHandle.AudioCh,
                Alpha = scene?.Flag.HasFlag(SceneFlag.Alpha) ?? false,
                SceneName = scene?.Name ?? string.Empty,
            };
            exo.Objects.AddRange(Objects.Where(obj => obj.SceneIndex == sceneIndex));
            exo.TrackbarScripts.AddRange(TrackbarScripts);

            return exo;
        }
    }
}
