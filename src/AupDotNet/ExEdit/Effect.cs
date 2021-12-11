using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Karoterra.AupDotNet.ExEdit
{
    public abstract class Effect
    {
        public EffectType Type { get; }
        public EffectFlag Flag { get; set; }

        public Trackbar[] Trackbars { get; }

        public int[] Checkboxes { get; }

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

        protected Effect(EffectType type, Trackbar[] trackbars, int[] checkboxes)
        {
            Type = type;
            if (trackbars.Length != type.TrackbarNum)
            {
                throw new ArgumentException("Trackbars' length is invalid");
            }
            Trackbars = trackbars.Clone() as Trackbar[];
            if (checkboxes.Length != type.CheckboxNum)
            {
                throw new ArgumentException("Checkboxes' length is invalid");
            }
            Checkboxes = checkboxes.Clone() as int[];
        }

        protected Effect(EffectType type, Trackbar[] trackbars, int[] checkboxes, ReadOnlySpan<byte> data)
            : this(type, trackbars, checkboxes)
        {
            ParseExtData(data);
        }

        protected virtual void ParseExtDataInternal(ReadOnlySpan<byte> data)
        {
        }

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

        public virtual byte[] DumpExtData()
        {
            return new byte[Type.ExtSize];
        }

        /// <summary>
        /// オブジェクトファイルを出力する。
        /// </summary>
        /// <param name="writer">出力先</param>
        /// <param name="trackbarScripts">ExEditProjectが持っているTrackbarScriptのリスト</param>
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
        /// オブジェクトファイルにEffectの拡張データを出力する
        /// </summary>
        /// <param name="writer">出力先</param>
        public virtual void ExportExtData(TextWriter writer)
        {
        }
    }
}
