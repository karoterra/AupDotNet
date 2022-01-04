using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;

namespace Karoterra.AupDotNet.ExEdit
{
    /// <summary>
    /// 拡張編集のオブジェクトファイル(*.exo)を表します。
    /// </summary>
    public class ExeditObjectFile
    {
        /// <summary>
        /// 横幅
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 高さ
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// フレームレート(分子)
        /// </summary>
        public int Rate { get; set; }

        /// <summary>
        /// フレームレート(分母)
        /// </summary>
        public int Scale { get; set; }

        /// <summary>
        /// フレーム数
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 音声のサンプリングレート
        /// </summary>
        public int AudioRate { get; set; }

        /// <summary>
        /// 音声のチャンネル数
        /// </summary>
        public int AudioCh { get; set; }

        /// <summary>
        /// アルファチャンネルあり
        /// </summary>
        public bool Alpha { get; set; }

        /// <summary>
        /// シーン名
        /// </summary>
        public string SceneName { get; set; }

        /// <summary>
        /// オブジェクト
        /// </summary>
        public List<TimelineObject> Objects { get; } = new List<TimelineObject>();

        /// <summary>
        /// トラックバー変化方法スクリプト
        /// </summary>
        public List<TrackbarScript> TrackbarScripts { get; } = new List<TrackbarScript>();

        /// <summary>
        /// <see cref="ExeditObjectFile"/> のインスタンスを初期化します。
        /// </summary>
        public ExeditObjectFile()
        {
        }

        /// <summary>
        /// オブジェクトファイルを出力します。
        /// </summary>
        /// <param name="writer">出力先</param>
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
