using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;

namespace AupDotNetTests.ExEdit
{
    [TestClass]
    public class TimelineObjectTest
    {
        public class ObjectData
        {
            public uint Size { get; set; }
            public TimelineObjectFlag Flag { get; set; }
            public uint StartFrame { get; set; }
            public uint EndFrame { get; set; }
            public string Preview { get; set; }
            public uint ChainGroup { get; set; }
            public bool Chain { get; set; }
            public uint ExtSize { get; set; }
            public uint Field0x4B8 { get; set; }
            public uint Group { get; set; }
            public uint LayerIndex { get; set; }
            public uint SceneIndex { get; set; }
            public int EffectCount { get; set; }
        }

        static void EqualTimelineObject(ObjectData expected, TimelineObject actual, string message = "")
        {
            Assert.AreEqual(expected.Size, actual.Size, message);
            Assert.AreEqual(expected.Flag, actual.Flag, message);
            Assert.AreEqual(expected.StartFrame, actual.StartFrame, message);
            Assert.AreEqual(expected.EndFrame, actual.EndFrame, message);
            Assert.AreEqual(expected.Preview, actual.Preview, message);
            Assert.AreEqual(expected.ChainGroup, actual.ChainGroup, message);
            Assert.AreEqual(expected.Chain, actual.Chain, message);
            Assert.AreEqual(expected.ExtSize, actual.ExtSize, message);
            Assert.AreEqual(expected.Field0x4B8, actual.Field0x4B8, message);
            Assert.AreEqual(expected.Group, actual.Group, message);
            Assert.AreEqual(expected.LayerIndex, actual.LayerIndex, message);
            Assert.AreEqual(expected.SceneIndex, actual.SceneIndex, message);
            Assert.AreEqual(expected.EffectCount, actual.Effects.Count, message);
        }

        [DataTestMethod]
        [DataRow(@"TestData\Exedit\EffectSet01.aup")]
        [DataRow(@"TestData\Exedit\ExeditBpm.aup")]
        [DataRow(@"TestData\Exedit\ExeditXY.aup")]
        [DataRow(@"TestData\Exedit\ExeditCamera.aup")]
        [DataRow(@"TestData\Exedit\Chain.aup")]
        [DataRow(@"TestData\Exedit\Group.aup")]
        [DataRow(@"TestData\Exedit\LayerScene.aup")]
        [DataRow(@"TestData\Exedit\Trackbar.aup")]
        public void Test_ReadDump(string filename)
        {
            AviUtlProject aup = new AviUtlProject(filename);
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);
            string jsonPath = Path.Combine(
                Path.GetDirectoryName(filename),
                Path.GetFileNameWithoutExtension(filename) + "_TimelineObject.json");
            string jsonText = File.ReadAllText(jsonPath);
            List<ObjectData> objects = JsonSerializer.Deserialize<List<ObjectData>>(jsonText);

            Assert.AreEqual(objects.Count, exedit.Objects.Count);
            for (int i = 0; i < objects.Count; i++)
            {
                EqualTimelineObject(objects[i], exedit.Objects[i], $"object {i}");
            }

            var dumped = exedit.DumpData();
            exedit = new ExEditProject(new RawFilterProject("拡張編集", dumped));
            Assert.AreEqual(objects.Count, exedit.Objects.Count);
            for (int i = 0; i < objects.Count; i++)
            {
                EqualTimelineObject(objects[i], exedit.Objects[i], $"object {i}");
            }
        }
    }
}
