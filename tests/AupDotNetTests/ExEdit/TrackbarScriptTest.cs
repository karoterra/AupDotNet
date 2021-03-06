using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;

namespace AupDotNetTests.ExEdit
{
    [TestClass]
    public class TrackbarScriptTest
    {
        [DataTestMethod]
        [DataRow("補間移動", "exedit.tra")]
        [DataRow("回転", "exedit.tra")]
        [DataRow("トラックバースクリプト", "トラックバースクリプト.tra")]
        [DataRow("その1@イージング", "@イージング.tra")]
        public void Test_Filename(string name, string filename)
        {
            var script = new TrackbarScript(name);
            Assert.AreEqual(filename, script.Filename);
        }

        [DataTestMethod]
        [DataRow(false, false, true, "--speed:0,0")]
        [DataRow(true, true, false, "--twopoint\r\n--param:100")]
        public void Test_ParseScript(bool twopoint, bool param, bool speed, string script)
        {
            var ts = new TrackbarScript();
            ts.ParseScript(script);
            Assert.AreEqual(twopoint, ts.EnableTwoPoint, "TwoPoint");
            Assert.AreEqual(param, ts.EnableParam, "Param");
            Assert.AreEqual(speed, ts.EnableSpeed, "Speed");
        }

        [DataTestMethod]
        [DataRow(@"TestData\Exedit\EffectSet01.aup")]
        [DataRow(@"TestData\Exedit\LayerScene.aup")]
        [DataRow(@"TestData\Exedit\Trackbar.aup")]
        public void Test_Read(string filename)
        {
            AviUtlProject aup = new AviUtlProject(filename);
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);
            string jsonPath = Path.Combine(
                Path.GetDirectoryName(filename),
                Path.GetFileNameWithoutExtension(filename) + "_trackbarScript.json");
            string jsonText = File.ReadAllText(jsonPath);
            List<TrackbarScript> trackbars = JsonSerializer.Deserialize<List<TrackbarScript>>(jsonText);

            Assert.AreEqual(trackbars.Count, exedit.TrackbarScripts.Count);
            for (int i = 0; i < trackbars.Count; i++)
            {
                Assert.AreEqual(trackbars[i].Name, exedit.TrackbarScripts[i].Name, $"{i}");
            }
        }
    }
}
