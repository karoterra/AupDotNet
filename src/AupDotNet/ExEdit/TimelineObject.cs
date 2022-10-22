using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit
{
    /// <summary>
    /// オブジェクト情報フラグ
    /// </summary>
    [Flags]
    public enum TimelineObjectFlag
    {
        /// <summary>オブジェクトが有効</summary>
        Enable = 1,

        /// <summary>上のオブジェクトでクリッピング</summary>
        Clipping = 0x0000_0100,
        /// <summary>カメラ制御の対象</summary>
        Camera = 0x0000_0200,

        /// <summary>メディアオブジェクト</summary>
        Media = 0x0001_0000,
        /// <summary>音声</summary>
        Audio = 0x0002_0000,
        /// <summary>メディアオブジェクト用のフィルタ効果、グループ制御</summary>
        MediaFilter = 0x0004_0000,
        /// <summary>時間制御、グループ制御、カメラ制御、カメラ制御用のフィルタ効果</summary>
        Control = 0x0008_0000,
        /// <summary>対象レイヤー範囲をタイムラインに表示</summary>
        Range = 0x0010_0000,
    }

    /// <summary>
    /// 拡張編集でタイムラインに配置するオブジェクトを表すクラス。
    /// </summary>
    public class TimelineObject
    {
        /// <summary>
        /// オブジェクトの基本バイナリサイズ。
        /// 拡張データのサイズはこれに含まれません。
        /// </summary>
        public static readonly int BaseSize = 0x5C8;

        /// <summary>
        /// 拡張データの合計サイズが格納されいている場所のオフセットアドレス。
        /// </summary>
        public static readonly int ExtSizeOffset = 0xF4;

        /// <summary>
        /// フィルタ効果の最大個数。
        /// </summary>
        public static readonly int MaxEffect = 12;

        /// <summary>
        /// プレビュー文字列の最大バイト数。
        /// </summary>
        public static readonly int MaxPreviewLength = 64;

        /// <summary>
        /// このオブジェクトがいずれの中間点グループにも属さない場合は <see cref="ChainGroup"/> はこの値になります。
        /// </summary>
        public static readonly uint NoChainGroup = 0xFFFF_FFFF;

        /// <summary>
        /// このオブジェクトがいずれのグループにも属さない場合は <see cref="Group"/> はこの値になります。
        /// </summary>
        public static readonly uint NoGroup = 0;

        /// <summary>
        /// オブジェクト情報をダンプした際のバイト長。
        /// </summary>
        public uint Size => (uint)(BaseSize + ExtSize);

        /// <summary>
        /// オブジェクトのフラグ
        /// </summary>
        public TimelineObjectFlag Flag { get; set; }

        /// <summary>
        /// 開始フレーム。
        /// </summary>
        public uint StartFrame { get; set; }

        /// <summary>
        /// 終了フレーム。
        /// </summary>
        public uint EndFrame { get; set; }

        private string _preview = string.Empty;
        /// <summary>
        /// プレビュー文字列。
        /// </summary>
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

        /// <summary>
        /// 中間点グループ。
        /// </summary>
        /// <remarks>
        /// この値が同じオブジェクトが同一の中間点グループに属する連結したオブジェクトとなります。
        /// 中間点で区切られたオブジェクトでない場合は <see cref="NoChainGroup"/> になります。
        /// </remarks>
        public uint ChainGroup { get; set; }

        /// <summary>
        /// 中間点で区切られたオブジェクトの後続か。
        /// </summary>
        /// <remarks>
        /// 中間点で区切られたオブジェクトの内、先頭以外のオブジェクトの場合に <c>true</c> になります。
        /// それ以外の場合は <c>false</c> になります。
        /// </remarks>
        public bool Chain { get; set; }

        /// <summary>
        /// 各フィルタ効果の拡張データの合計。
        /// </summary>
        /// <remarks>
        /// 中間点で区切られたオブジェクトの後続の場合、この値は 0 になります。
        /// </remarks>
        public uint ExtSize => Chain ? 0 : (uint)Effects.Sum(x => x.Type.ExtSize);

        /// <summary>
        /// オフセットアドレス 0x4B8
        /// </summary>
        public uint Field0x4B8 { get; set; }

        /// <summary>
        /// グループ番号。
        /// </summary>
        /// <remarks>
        /// この値が同じオブジェクトが同一のグループに属するオブジェクトとなります。
        /// いずれのグループにも属さない場合は <see cref="NoGroup"/> になります。
        /// </remarks>
        public uint Group { get; set; }

        /// <summary>
        /// レイヤー番号。
        /// </summary>
        public uint LayerIndex { get; set; }

        /// <summary>
        /// シーン番号。
        /// </summary>
        public uint SceneIndex { get; set; }

        /// <summary>
        /// フィルタ効果。
        /// </summary>
        public readonly List<Effect> Effects = new();

        /// <summary>
        /// <see cref="TimelineObject"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="data">オブジェクト情報</param>
        /// <param name="lastChainGroup">中間点グループ</param>
        /// <param name="effectTypes">フィルタ効果定義</param>
        /// <param name="effectFactory">フィルタ効果のファクトリ</param>
        public TimelineObject(ReadOnlySpan<byte> data, uint lastChainGroup, IReadOnlyList<EffectType> effectTypes, IEffectFactory? effectFactory = null)
        {
            if (effectFactory == null) effectFactory = new EffectFactory();

            Flag = (TimelineObjectFlag)(data.Slice(0, 4).ToUInt32());
            StartFrame = data.Slice(8, 4).ToUInt32();
            EndFrame = data.Slice(12, 4).ToUInt32();
            Preview = data.Slice(0x10, 64).ToCleanSjisString();
            ChainGroup = data.Slice(0x50, 4).ToUInt32();
            var extSize = data.Slice(0xF4, 4).ToUInt32();
            Chain = ChainGroup != 0xFFFF_FFFF && ChainGroup == lastChainGroup && extSize == 0;
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
                var extData = Chain ? Array.Empty<byte>() : data.Slice(BaseSize + offset, (int)type.ExtSize).ToArray();
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

        /// <summary>
        /// オブジェクトをダンプします。
        /// </summary>
        /// <param name="data">オブジェクト情報を格納する配列</param>
        /// <param name="editingScene">現在編集中のシーン</param>
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
        /// オブジェクト情報をオブジェクトファイルに出力します。
        /// </summary>
        /// <param name="writer">出力先</param>
        /// <param name="index">オブジェクトのインデックス</param>
        /// <param name="trackbarScripts">ExEditProjectが持っているTrackbarScriptのリスト</param>
        /// <param name="chainParent">中間点グループの先頭オブジェクト。自分が先頭あるいは中間点で区切られていない場合は null</param>
        public void ExportObject(TextWriter writer, int index, IReadOnlyList<TrackbarScript> trackbarScripts, TimelineObject? chainParent)
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
                Effects[i].Export(writer, trackbarScripts, Chain, chainParent?.Effects[i]);
            }
        }
    }
}
