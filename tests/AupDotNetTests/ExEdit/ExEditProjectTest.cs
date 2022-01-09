using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.Extensions;

namespace AupDotNetTests.ExEdit
{
    [TestClass]
    public class ExEditProjectTest
    {
        public class ExeditData
        {
            public uint Field0xC { get; set; }
            public uint Zoom { get; set; }
            public uint Field0x14 { get; set; }
            public uint EditingObject { get; set; }
            public uint Field0x1C { get; set; }
            public uint Field0x20 { get; set; }
            public uint Field0x24 { get; set; }
            public uint Field0x28 { get; set; }
            public uint Version { get; set; }
            public bool EnableBpmGrid { get; set; }
            public double BpmGridTempo { get; set; }
            public uint BpmGridOffset { get; set; }
            public bool EnableXYGrid { get; set; }
            public uint XYGridWidth { get; set; }
            public uint XYGridHeight { get; set; }
            public bool EnableCameraGrid { get; set; }
            public uint CameraGridWidth { get; set; }
            public uint CameraGridHeight { get; set; }
            public bool ShowOutsideFrame { get; set; }
            public uint OutsideFrameScale { get; set; }
            public uint BpmGridBeat { get; set; }
            public uint Field0x60 { get; set; }
            public uint EditingScene { get; set; }
            public uint Field0x78 { get; set; }

            public byte[] Field0x80_0xFF { get; set; } = new byte[128];

            public int LayerCount { get; set; }
            public int SceneCount { get; set; }
            public int TrackbarScriptCount { get; set; }
            public int EffectTypeCount { get; set; }
            public int ObjectCount { get; set; }
        }

        static void ExeditEqual(ExEditProject a, ExEditProject b)
        {
            Assert.AreEqual(a.Field0xC, b.Field0xC);
            Assert.AreEqual(a.Zoom, b.Zoom);
            Assert.AreEqual(a.Field0x14, b.Field0x14);
            Assert.AreEqual(a.EditingObject, b.EditingObject);
            Assert.AreEqual(a.Field0x1C, b.Field0x1C);
            Assert.AreEqual(a.Field0x20, b.Field0x20);
            Assert.AreEqual(a.Field0x24, b.Field0x24);
            Assert.AreEqual(a.Field0x28, b.Field0x28);
            Assert.AreEqual(a.Version, b.Version);
            Assert.AreEqual(a.EnableBpmGrid, b.EnableBpmGrid);
            Assert.AreEqual(a.BpmGridTempo, b.BpmGridTempo);
            Assert.AreEqual(a.BpmGridOffset, b.BpmGridOffset);
            Assert.AreEqual(a.EnableXYGrid, b.EnableXYGrid);
            Assert.AreEqual(a.XYGridWidth, b.XYGridWidth);
            Assert.AreEqual(a.XYGridHeight, b.XYGridHeight);
            Assert.AreEqual(a.EnableCameraGrid, b.EnableCameraGrid);
            Assert.AreEqual(a.CameraGridWidth, b.CameraGridWidth);
            Assert.AreEqual(a.CameraGridHeight, b.CameraGridHeight);
            Assert.AreEqual(a.ShowOutsideFrame, b.ShowOutsideFrame);
            Assert.AreEqual(a.OutsideFrameScale, b.OutsideFrameScale);
            Assert.AreEqual(a.BpmGridBeat, b.BpmGridBeat);
            Assert.AreEqual(a.Field0x60, b.Field0x60);
            Assert.AreEqual(a.EditingScene, b.EditingScene);
            Assert.AreEqual(a.Field0x78, b.Field0x78);
            Assert.IsTrue(a.Field0x80_0xFF.SequenceEqual(b.Field0x80_0xFF));
        }

        [DataTestMethod]
        [DataRow(@"TestData\FilterProject\VariousFilters.aup")]
        [DataRow(@"TestData\Exedit\EffectSet01.aup")]
        [DataRow(@"TestData\Exedit\ExeditBpm.aup")]
        [DataRow(@"TestData\Exedit\ExeditXY.aup")]
        [DataRow(@"TestData\Exedit\ExeditCamera.aup")]
        [DataRow(@"TestData\Exedit\Chain.aup")]
        [DataRow(@"TestData\Exedit\Group.aup")]
        [DataRow(@"TestData\Exedit\LayerScene.aup")]
        [DataRow(@"TestData\Exedit\Trackbar.aup")]
        public void Test_Read(string filename)
        {
            AviUtlProject aup = new AviUtlProject(filename);
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);
            string jsonPath = Path.Combine(
                Path.GetDirectoryName(filename),
                $"{Path.GetFileNameWithoutExtension(filename)}_Exedit.json");
            string json = File.ReadAllText(jsonPath);
            var data = JsonSerializer.Deserialize<ExeditData>(json);

            Assert.AreEqual(data.Field0xC, exedit.Field0xC);
            Assert.AreEqual(data.Zoom, exedit.Zoom);
            Assert.AreEqual(data.Field0x14, exedit.Field0x14);
            Assert.AreEqual(data.EditingObject, exedit.EditingObject);
            Assert.AreEqual(data.Field0x1C, exedit.Field0x1C);
            Assert.AreEqual(data.Field0x20, exedit.Field0x20);
            Assert.AreEqual(data.Field0x24, exedit.Field0x24);
            Assert.AreEqual(data.Field0x28, exedit.Field0x28);
            Assert.AreEqual(data.Version, exedit.Version);
            Assert.AreEqual(data.EnableBpmGrid, exedit.EnableBpmGrid);
            Assert.AreEqual(data.BpmGridTempo, exedit.BpmGridTempo);
            Assert.AreEqual(data.BpmGridOffset, exedit.BpmGridOffset);
            Assert.AreEqual(data.EnableXYGrid, exedit.EnableXYGrid);
            Assert.AreEqual(data.XYGridWidth, exedit.XYGridWidth);
            Assert.AreEqual(data.XYGridHeight, exedit.XYGridHeight);
            Assert.AreEqual(data.EnableCameraGrid, exedit.EnableCameraGrid);
            Assert.AreEqual(data.CameraGridWidth, exedit.CameraGridWidth);
            Assert.AreEqual(data.CameraGridHeight, exedit.CameraGridHeight);
            Assert.AreEqual(data.ShowOutsideFrame, exedit.ShowOutsideFrame);
            Assert.AreEqual(data.OutsideFrameScale, exedit.OutsideFrameScale);
            Assert.AreEqual(data.BpmGridBeat, exedit.BpmGridBeat);
            Assert.AreEqual(data.Field0x60, exedit.Field0x60);
            Assert.AreEqual(data.EditingScene, exedit.EditingScene);
            Assert.AreEqual(data.Field0x78, exedit.Field0x78);
            CollectionAssert.AreEqual(data.Field0x80_0xFF, exedit.Field0x80_0xFF);
            Assert.AreEqual(data.LayerCount, exedit.Layers.Count);
            Assert.AreEqual(data.SceneCount, exedit.Scenes.Count);
            Assert.AreEqual(data.TrackbarScriptCount, exedit.TrackbarScripts.Count);
            Assert.AreEqual(data.EffectTypeCount, exedit.EffectTypes.Count);
            Assert.AreEqual(data.ObjectCount, exedit.Objects.Count);
        }

        [DataTestMethod]
        [DataRow(@"TestData\FilterProject\VariousFilters.aup")]
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
            RawFilterProject raw = aup.FilterProjects
                .Where(f => f.Name == "拡張編集")
                .First() as RawFilterProject;
            ExEditProject exedit = new ExEditProject(raw);
            var dst = raw.DumpData();

            Assert.IsTrue(raw.Data.Take(0x100).SequenceEqual(dst.Take(0x100)));
        }

        [DataTestMethod]
        [DataRow(@"TestData\FilterProject\VariousFilters.aup")]
        [DataRow(@"TestData\Exedit\EffectSet01.aup")]
        [DataRow(@"TestData\Exedit\ExeditBpm.aup")]
        [DataRow(@"TestData\Exedit\ExeditXY.aup")]
        [DataRow(@"TestData\Exedit\ExeditCamera.aup")]
        [DataRow(@"TestData\Exedit\Chain.aup")]
        [DataRow(@"TestData\Exedit\Group.aup")]
        [DataRow(@"TestData\Exedit\LayerScene.aup")]
        [DataRow(@"TestData\Exedit\Trackbar.aup")]
        public void Test_ReadDumpRead(string filename)
        {
            AviUtlProject aup = new AviUtlProject(filename);
            ExEditProject src = ExeditTestUtil.GetExEdit(aup);
            var data = src.DumpData();
            RawFilterProject rawFilter = new RawFilterProject("拡張編集", data);
            ExEditProject dst = new ExEditProject(rawFilter);

            ExeditEqual(src, dst);

            Assert.AreEqual(src.Layers.Count, dst.Layers.Count);
            Assert.AreEqual(src.Scenes.Count, dst.Scenes.Count);
            Assert.AreEqual(src.TrackbarScripts.Count, dst.TrackbarScripts.Count);
            Assert.AreEqual(src.EffectTypes.Count, dst.EffectTypes.Count);
            Assert.AreEqual(src.Objects.Count, dst.Objects.Count);
        }

        [DataTestMethod]
        [DataRow(@"TestData\FilterProject\VariousFilters.aup")]
        [DataRow(@"TestData\Exedit\EffectSet01.aup")]
        [DataRow(@"TestData\Exedit\ExeditBpm.aup")]
        [DataRow(@"TestData\Exedit\ExeditXY.aup")]
        [DataRow(@"TestData\Exedit\ExeditCamera.aup")]
        [DataRow(@"TestData\Exedit\Chain.aup")]
        [DataRow(@"TestData\Exedit\Group.aup")]
        [DataRow(@"TestData\Exedit\LayerScene.aup")]
        [DataRow(@"TestData\Exedit\Trackbar.aup")]
        public void Test_ReadDumpReadDump(string filename)
        {
            AviUtlProject aup = new AviUtlProject(filename);
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);
            var expected = exedit.DumpData();
            RawFilterProject rawFilter = new RawFilterProject("拡張編集", expected);
            exedit = new ExEditProject(rawFilter);
            var actual = exedit.DumpData();
            Assert.IsTrue(expected.SequenceEqual(actual));
        }

        [TestMethod]
        public void Test_ReadInvalidData()
        {
            var exedit = new ExEditProject();
            Assert.ThrowsException<IndexOutOfRangeException>(() =>
            {
                exedit.Read(Array.Empty<byte>());
            });
            Assert.ThrowsException<ArgumentException>(() =>
            {
                exedit.Read(new byte[] {0, 0, 0, 0});
            });
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                exedit.Read("80EE".ToSjisBytes());
            });
        }
    }
}
