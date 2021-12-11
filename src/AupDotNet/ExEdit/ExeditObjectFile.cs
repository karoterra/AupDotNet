using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;

namespace Karoterra.AupDotNet.ExEdit
{
    /// <summary>
    /// オブジェクトファイル(*.exo)
    /// </summary>
    public class ExeditObjectFile
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Rate { get; set; }
        public int Scale { get; set; }
        public int Length { get; set; }
        public int AudioRate { get; set; }
        public int AudioCh { get; set; }

        public bool Alpha { get; set; }
        public string SceneName { get; set; }

        public List<TimelineObject> Objects { get; } = new List<TimelineObject>();

        public List<TrackbarScript> TrackbarScripts { get; } = new List<TrackbarScript>();

        public ExeditObjectFile()
        {
        }

        public void Write(TextWriter writer)
        {
            writer.WriteLine("[exedit]");
            writer.Write("width=");
            writer.WriteLine(Width);
            writer.Write("height=");
            writer.WriteLine(Height);
            writer.Write("rate=");
            writer.WriteLine(Rate);
            writer.Write("scale=");
            writer.WriteLine(Scale);
            writer.Write("length=");
            writer.WriteLine(Length);
            writer.Write("audio_rate=");
            writer.WriteLine(AudioRate);
            writer.Write("audio_ch=");
            writer.WriteLine(AudioCh);

            if (Alpha)
                writer.WriteLine("alpha=1");
            if (!string.IsNullOrEmpty(SceneName))
            {
                writer.Write("name=");
                writer.WriteLine(SceneName);
            }

            for (int i = 0; i < Objects.Count; i++)
            {
                Objects[i].ExportObject(writer, i, TrackbarScripts);
            }
        }
    }
}
