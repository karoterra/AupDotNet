using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;

namespace AupDotNetTests.ExEdit
{
    [TestClass]
    public class ExeditObjectFileTest
    {
        [DataTestMethod]
        [DataRow(@"TestData\Exedit\EffectSet01.aup", 0)]
        [DataRow(@"TestData\Exedit\LayerScene.aup", 0)]
        [DataRow(@"TestData\Exedit\LayerScene.aup", 1)]
        [DataRow(@"TestData\Exedit\LayerScene.aup", 2)]
        [DataRow(@"TestData\Exedit\Trackbar.aup", 0)]
        [DataRow(@"TestData\Exedit\Chain.aup", 0)]
        [DataRow(@"TestData\Exedit\Chain.aup", 1)]
        [DataRow(@"TestData\Exedit\Group.aup", 0)]
        [DataRow(@"TestData\Exedit\Group.aup", 1)]
        public void Test_Write(string filename, int scene)
        {
            AviUtlProject aup = new AviUtlProject(filename);
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var ts = exedit.TrackbarScripts.Where(x => x.Name == "トラックバーサンプル").FirstOrDefault();
            if (ts != null)
            {
                ts.EnableParam = true;
                ts.EnableSpeed = false;
            }

            ExeditObjectFile exo = exedit.ExportObject(scene, aup.EditHandle);

            var exoPath = Path.Combine(
                Path.GetDirectoryName(filename),
                $"{Path.GetFileNameWithoutExtension(filename)}_{scene}.exo");
            var expected = File.ReadAllLines(exoPath, Encoding.GetEncoding("shift-jis"));

            using (var writer = new StringWriter())
            {
                writer.NewLine = "\r\n";
                exo.Write(writer);

                var actual = writer.ToString().Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < expected.Length; i++)
                {
                    Assert.AreEqual(expected[i], actual[i], $"Line {i + 1}");
                }
            }
        }
    }
}
