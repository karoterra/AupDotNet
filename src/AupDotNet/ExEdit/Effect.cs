using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Karoterra.AupDotNet.ExEdit
{
    /// <summary>
    /// フィルタ効果を表す抽象クラス。
    /// </summary>
    public abstract class Effect
    {
        /// <summary>
        /// フィルタ効果の定義。
        /// </summary>
        public EffectType Type { get; }

        /// <summary>
        /// フィルタ効果のフラグ。
        /// </summary>
        public EffectFlag Flag { get; set; }

        /// <summary>
        /// トラックバー。
        /// </summary>
        public Trackbar[] Trackbars { get; }

        /// <summary>
        /// チェックボックス。
        /// </summary>
        public int[] Checkboxes { get; }

        /// <summary>
        /// <see cref="Effect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="type">フィルタ効果の定義</param>
        protected Effect(EffectType type)
        {
            Type = type;
            Trackbars = new Trackbar[Type.TrackbarNum];
            foreach (var (def, i) in type.Trackbars.Select((def, i) => (def, i)))
            {
                if (def == null)
                    Trackbars[i] = new Trackbar();
                else
                    Trackbars[i] = new Trackbar(def.Default, def.Default, 0, 0);
            }
            Checkboxes = new int[Type.CheckboxNum];
            foreach (var (def, i) in type.Checkboxes
                .Select((def, i) => (def, i))
                .Where(x => x.def != null && x.def.IsCheckbox))
            {
                Checkboxes[i] = def.Default;
            }
        }

        /// <summary>
        /// <see cref="Effect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="type">フィルタ効果の定義</param>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <exception cref="ArgumentException">配列の長さが正しくありません。</exception>
        protected Effect(EffectType type, Trackbar[] trackbars, int[] checkboxes)
        {
            Type = type;
            if (trackbars.Length != type.TrackbarNum)
            {
                throw new ArgumentException("Trackbars' length is invalid");
            }
            Trackbars = trackbars;
            if (checkboxes.Length != type.CheckboxNum)
            {
                throw new ArgumentException("Checkboxes' length is invalid");
            }
            Checkboxes = checkboxes;
        }

        /// <summary>
        /// <see cref="Effect"/> のインスタンスを初期化します。
        /// </summary>
        /// <param name="type">フィルタ効果の定義</param>
        /// <param name="trackbars">トラックバー</param>
        /// <param name="checkboxes">チェックボックス</param>
        /// <param name="data">拡張データ</param>
        protected Effect(EffectType type, Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : this(type, trackbars, checkboxes)
        {
            ParseExtData(data);
        }

        /// <summary>
        /// 拡張データをパースします。
        /// 拡張データをもつフィルタ効果の派生クラスはこのメソッドをオーバーロードしてください。
        /// </summary>
        /// <param name="data">拡張データ</param>
        protected virtual void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
        }

        /// <summary>
        /// 拡張データをパースします。
        /// </summary>
        /// <param name="data">拡張データ</param>
        /// <exception cref="ArgumentException">拡張データの長さが正しくありません。</exception>
        public void ParseExtData(ReadOnlySpan<byte> data)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    ParseExtDataInternal(data);
                }
                else if (data.Length != 0)
                {
                    throw new ArgumentException("data's length is invalid.");
                }
            }
        }

        /// <summary>
        /// 拡張データをダンプします。
        /// 拡張データをもつフィルタ効果の派生クラスはこのメソッドをオーバーロードしてください。
        /// </summary>
        /// <returns>ダンプされたデータ</returns>
        public virtual byte[] DumpExtData()
        {
            return new byte[Type.ExtSize];
        }

        /// <summary>
        /// オブジェクトファイルにフィルタ効果の情報を出力します。
        /// </summary>
        /// <param name="writer">出力先</param>
        /// <param name="trackbarScripts">ExEditProjectが持っているTrackbarScriptのリスト</param>
        /// <param name="chain">中間点で区切られたオブジェクトの後続オブジェクトであるか。<see cref="TimelineObject.Chain"/> の値。</param>
        public void Export(TextWriter writer, IReadOnlyList<TrackbarScript> trackbarScripts, bool chain)
        {
            writer.Write("_name=");
            writer.WriteLine(Type.Name);
            for (int i = 0; i < Trackbars.Length; i++)
            {
                var def = Type.Trackbars[i];
                if (def == null || string.IsNullOrEmpty(def.Name)) continue;

                writer.Write(def.Name);
                writer.Write('=');
                var script = trackbarScripts[Trackbars[i].ScriptIndex];
                writer.WriteLine(Trackbars[i].ToString(def.Scale, script));
            }
            for (int i = 0; i < Checkboxes.Length; i++)
            {
                var def = Type.Checkboxes[i];
                if (def == null || !def.IsCheckbox) continue;

                writer.Write(def.Name);
                writer.Write('=');
                writer.WriteLine(Checkboxes[i]);
            }
            if (!chain)
            {
                ExportExtData(writer);
            }
        }

        /// <summary>
        /// オブジェクトファイルにEffectの拡張データを出力します。
        /// 拡張データをもつフィルタ効果の派生クラスはこのメソッドをオーバーロードしてください。
        /// </summary>
        /// <param name="writer">出力先</param>
        public virtual void ExportExtData(TextWriter writer)
        {
        }
    }
}
