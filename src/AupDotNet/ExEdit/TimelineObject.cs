using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Karoterra.AupDotNet.ExEdit.Effects;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit
{
    [Flags]
    public enum TimelineObjectFlag
    {
        Enable = 1,

        Clipping = 0x0000_0100,
        Camera = 0x0000_0200,

        Media = 0x0001_0000,
        Audio = 0x0002_0000,
        MediaFilter = 0x0004_0000,
        Control = 0x0008_0000,
        Range = 0x0010_0000,
    }

    public class TimelineObject
    {
        public static readonly int BaseSize = 0x5C8;
        public static readonly int ExtSizeOffset = 0xF4;
        public static readonly int MaxEffect = 12;
        public static readonly int MaxPreviewLength = 64;

        public static readonly uint NoChainGroup = 0xFFFF_FFFF;
        public static readonly uint NoGroup = 0;

        public uint Size => (uint)(BaseSize + ExtSize);

        public TimelineObjectFlag Flag { get; set; }
        public uint StartFrame { get; set; }
        public uint EndFrame { get; set; }

        private string _preview;
        public string Preview
        {
            get => _preview;
            set
            {
                if (value.GetSjisByteCount() >= MaxPreviewLength)
                {
                    throw new MaxByteCountOfStringException(nameof(Preview), MaxPreviewLength);
                }
                _preview = value;
            }
        }

        public uint ChainGroup { get; set; }
        public bool Chain { get; set; }

        public uint ExtSize => Chain ? 0 : (uint)Effects.Sum(x => x.Type.ExtSize);

        public uint Field0x4B8 { get; set; }
        public uint Group { get; set; }
        public uint LayerIndex { get; set; }
        public uint SceneIndex { get; set; }

        public readonly List<Effect> Effects = new List<Effect>();

        public TimelineObject(ReadOnlySpan<byte> data, IReadOnlyList<EffectType> effectTypes, IEffectFactory effectFactory = null)
        {
            if (effectFactory == null) effectFactory = new EffectFactory();

            Flag = (TimelineObjectFlag)(data.Slice(0, 4).ToUInt32());
            StartFrame = data.Slice(8, 4).ToUInt32();
            EndFrame = data.Slice(12, 4).ToUInt32();
            Preview = data.Slice(0x10, 64).ToCleanSjisString();
            ChainGroup = data.Slice(0x50, 4).ToUInt32();
            var extSize = data.Slice(0xF4, 4).ToUInt32();
            Chain = (ChainGroup != 0xFFFF_FFFF && extSize == 0) ? true : false;
            Field0x4B8 = data.Slice(0x4B8, 4).ToUInt32();
            Group = data.Slice(0x4BC, 4).ToUInt32();
            LayerIndex = data.Slice(0x5C0, 4).ToUInt32();
            SceneIndex = data.Slice(0x5C4, 4).ToUInt32();

            Effects.Clear();
            int trackbarCount = 0;
            int checkboxCount = 0;
            for (int i = 0; i < MaxEffect; i++)
            {
                var typeIndex = data.Slice(0x54 + i * 12, 4).ToInt32();
                if (typeIndex == -1)
                {
                    break;
                }
                var type = effectTypes[typeIndex];
                var offset = data.Slice(0x54 + i * 12 + 8, 4).ToInt32();
                var extData = Chain ? new byte[0] : data.Slice(BaseSize + offset, (int)type.ExtSize).ToArray();
                var flag = (EffectFlag)data[0xE4 + i];

                var trackbars = new Trackbar[type.TrackbarNum];
                for (int j = 0; j < trackbars.Length; j++)
                {
                    var index = trackbarCount + j;
                    var current = data.Slice(0xF8 + index * 4, 4).ToInt32();
                    var next = data.Slice(0x1F8 + index * 4, 4).ToInt32();
                    var transition = data.Slice(0x2F8 + index * 4, 4).ToUInt32();
                    var param = data.Slice(0x4C0 + index * 4, 4).ToInt32();
                    trackbars[j] = new Trackbar(current, next, transition, param);
                }

                var checkboxes = new int[type.CheckboxNum];
                for (int j = 0; j < checkboxes.Length; j++)
                {
                    var index = checkboxCount + j;
                    checkboxes[j] = data.Slice(0x3F8 + index * 4, 4).ToInt32();
                }

                Effect effect = effectFactory.Create(type, trackbars, checkboxes, extData);
                effect.Flag = flag;
                Effects.Add(effect);
                trackbarCount += trackbars.Length;
                checkboxCount += (int)type.CheckboxNum;
            }
        }

        public void Dump(Span<byte> data, uint editingScene)
        {
            ((uint)Flag).ToBytes().CopyTo(data);
            var field0x4 = (SceneIndex == editingScene) ? LayerIndex : 0xFFFF_FFFF;
            field0x4.ToBytes().CopyTo(data.Slice(4));
            StartFrame.ToBytes().CopyTo(data.Slice(8));
            EndFrame.ToBytes().CopyTo(data.Slice(12));
            Preview.ToSjisBytes(MaxPreviewLength).CopyTo(data.Slice(0x10, MaxPreviewLength));
            ChainGroup.ToBytes().CopyTo(data.Slice(0x50));
            ExtSize.ToBytes().CopyTo(data.Slice(0xF4));
            Field0x4B8.ToBytes().CopyTo(data.Slice(0x4B8));
            Group.ToBytes().CopyTo(data.Slice(0x4BC));
            LayerIndex.ToBytes().CopyTo(data.Slice(0x5C0));
            SceneIndex.ToBytes().CopyTo(data.Slice(0x5C4));

            var extCursor = 0;
            var trackbarCount = 0;
            var checkboxCount = 0;
            for (int i = 0; i < Effects.Count; i++)
            {
                Effects[i].Type.Id.ToBytes().CopyTo(data.Slice(0x54 + i * 12));
                ((ushort)trackbarCount).ToBytes().CopyTo(data.Slice(0x54 + i * 12 + 4));
                ((ushort)checkboxCount).ToBytes().CopyTo(data.Slice(0x54 + i * 12 + 6));
                extCursor.ToBytes().CopyTo(data.Slice(0x54 + i * 12 + 8));
                data[0xE4 + i] = (byte)Effects[i].Flag;

                for (int j = 0; j < Effects[i].Trackbars.Length; j++)
                {
                    var trackbar = Effects[i].Trackbars[j];
                    var index = trackbarCount + j;
                    trackbar.Current.ToBytes().CopyTo(data.Slice(0xF8 + index * 4));
                    trackbar.Next.ToBytes().CopyTo(data.Slice(0x1F8 + index * 4));
                    trackbar.Transition.ToBytes().CopyTo(data.Slice(0x2F8 + index * 4));
                    trackbar.Parameter.ToBytes().CopyTo(data.Slice(0x4C0 + index * 4));
                }

                for (int j = 0; j < Effects[i].Checkboxes.Length; j++)
                {
                    var index = checkboxCount + j;
                    Effects[i].Checkboxes[j].ToBytes().CopyTo(data.Slice(0x3F8 + index * 4));
                }

                if (!Chain)
                {
                    var extData = Effects[i].DumpExtData();
                    extData.CopyTo(data.Slice(BaseSize + extCursor));
                    extCursor += extData.Length;
                }

                trackbarCount += (int)Effects[i].Type.TrackbarNum;
                checkboxCount += (int)Effects[i].Type.CheckboxNum;
            }
            ((ushort)trackbarCount).ToBytes().CopyTo(data.Slice(0xF0));
            ((ushort)checkboxCount).ToBytes().CopyTo(data.Slice(0xF2));
            for (int i = Effects.Count; i < MaxEffect; i++)
            {
                0xFFFF_FFFF.ToBytes().CopyTo(data.Slice(0x54 + i * 12));
            }
        }

        /// <summary>
        /// オブジェクトファイルを出力する
        /// </summary>
        /// <param name="writer">出力先</param>
        /// <param name="index">オブジェクトのインデックス</param>
        /// <param name="trackbarScripts">ExEditProjectが持っているTrackbarScriptのリスト</param>
        public void ExportObject(TextWriter writer, int index, IReadOnlyList<TrackbarScript> trackbarScripts)
        {
            writer.Write('[');
            writer.Write(index);
            writer.WriteLine(']');
            writer.Write("start=");
            writer.WriteLine(StartFrame + 1);
            writer.Write("end=");
            writer.WriteLine(EndFrame + 1);
            writer.Write("layer=");
            writer.WriteLine(LayerIndex + 1);
            if (Group != NoGroup)
            {
                writer.Write("group=");
                writer.WriteLine(Group);
            }

            bool media = Flag.HasFlag(TimelineObjectFlag.Media);
            bool audio = Flag.HasFlag(TimelineObjectFlag.Audio);
            bool mediaFilter = Flag.HasFlag(TimelineObjectFlag.MediaFilter);
            bool control = Flag.HasFlag(TimelineObjectFlag.Control);
            if (media || mediaFilter)
            {
                writer.Write("overlay=");
                writer.WriteLine(Flag.HasFlag(TimelineObjectFlag.Clipping) ? 0 : 1);
            }
            if ((media && !audio) || (mediaFilter && control))
            {
                writer.Write("camera=");
                writer.WriteLine(Flag.HasFlag(TimelineObjectFlag.Camera) ? 1 : 0);
            }
            if (audio)
                writer.WriteLine("audio=1");

            if (Chain)
                writer.WriteLine("chain=1");

            for (int i = 0; i < Effects.Count; i++)
            {
                writer.Write('[');
                writer.Write(index);
                writer.Write('.');
                writer.Write(i);
                writer.WriteLine(']');
                Effects[i].Export(writer, trackbarScripts, Chain);
            }
        }
    }
}
