using System;
using System.IO;
using System.Text;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;

namespace ReadWriteExEdit
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("ファイル名を指定してください");
                return 1;
            }
            string inputPath = args[0];
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"\"{inputPath}\"が見つかりません");
                return 1;
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var aup = new AviUtlProject();
            using (var reader = new BinaryReader(File.OpenRead(inputPath)))
            {
                aup.Read(reader);
            }
            ExEditProject exedit = null;
            for (int i = 0; i < aup.FilterProjects.Count; i++)
            {
                if (aup.FilterProjects[i].Name == "拡張編集")
                {
                    exedit = new ExEditProject(aup.FilterProjects[i] as RawFilterProject);
                    aup.FilterProjects[i] = exedit;
                    break;
                }
            }

            if (exedit != null)
            {
                Console.WriteLine($"バージョン: {exedit.Version}");
                Console.WriteLine($"タイムラインのズームレベル: {exedit.Zoom}");
                Console.WriteLine($"グリッド(BPM): {exedit.EnableBpmGrid} BPM:{exedit.BpmGridTempo} 拍子:{exedit.BpmGridBeat} 基準フレーム:{exedit.BpmGridOffset}");
                Console.WriteLine($"グリッド(XY軸): {exedit.EnableXYGrid} 横幅:{exedit.XYGridWidth} 縦幅:{exedit.XYGridHeight}");
                Console.WriteLine($"グリッド(カメラ制御): {exedit.EnableCameraGrid} 横:{exedit.CameraGridWidth} 縦:{exedit.CameraGridHeight}");
                Console.WriteLine($"フレーム領域外を表示: {exedit.ShowOutsideFrame}");
                Console.WriteLine($"編集中のオブジェクト: {exedit.EditingObject}");
                Console.WriteLine($"編集中のシーン: {exedit.EditingScene}");

                Console.WriteLine($"レイヤー ({exedit.Layers.Count})");
                foreach (var layer in exedit.Layers)
                {
                    Console.WriteLine($"  Scene:{layer.SceneIndex} Layer:{layer.LayerIndex} Name:{layer.Name} Flag:{layer.Flag}");
                }
                Console.WriteLine($"シーン ({exedit.Scenes.Count})");
                foreach (var scenes in exedit.Scenes)
                {
                    Console.WriteLine($"  Scene:{scenes.SceneIndex} Name:{scenes.Name} Flag:{scenes.Flag} Height:{scenes.Height} Width:{scenes.Width}");
                }
                Console.WriteLine($"トラックバースクリプト ({exedit.TrackbarScripts.Length})");
                foreach (var ts in exedit.TrackbarScripts)
                {
                    Console.WriteLine($"  {ts.Name}");
                }
                Console.WriteLine($"オブジェクトの種類 ({exedit.FilterTypes.Length})");
                foreach (var ft in exedit.FilterTypes)
                {
                    Console.WriteLine($"  {ft.Name}");
                }
                Console.WriteLine($"オブジェクト ({exedit.Objects.Count})");
                foreach (var obj in exedit.Objects)
                {
                    var name = (obj.Preview.Length != 0) ? obj.Preview.Replace("\r\n", " ") : obj.Filters[0].Type.Name;
                    Console.WriteLine($"  Scene:{obj.SceneIndex} Layer:{obj.LayerIndex} {name}");
                    foreach (var filter in obj.Filters)
                    {
                        Console.WriteLine($"    {filter.Type.Name}");
                    }
                }

                var outputPath = Path.Combine(
                Path.GetDirectoryName(inputPath),
                Path.GetFileNameWithoutExtension(inputPath) + "_parsed.aup");
                using (var writer = new BinaryWriter(File.OpenWrite(outputPath)))
                {
                    aup.Write(writer);
                }
            }

            return 0;
        }
    }
}
