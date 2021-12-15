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
    public class SceneTest
    {
        [DataTestMethod]
        [DataRow(@"TestData\Exedit\EffectSet01.aup")]
        [DataRow(@"TestData\Exedit\LayerScene.aup")]
        public void Test_Read(string filename)
        {
            AviUtlProject aup = new AviUtlProject(filename);
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);
            string jsonPath = Path.Combine(
                Path.GetDirectoryName(filename),
                Path.GetFileNameWithoutExtension(filename) + "_scene.json");
            string jsonText = File.ReadAllText(jsonPath);
            List<Scene> scenes = JsonSerializer.Deserialize<List<Scene>>(jsonText);

            Assert.AreEqual(scenes.Count, exedit.Scenes.Count);
            for (int i = 0; i < scenes.Count; i++)
            {
                Scene expected = scenes[i], actual = exedit.Scenes[i];
                Assert.AreEqual(expected.SceneIndex, actual.SceneIndex, $"{i}: SceneIndex");
                Assert.AreEqual(expected.Flag, actual.Flag, $"{i}: Flag");
                Assert.AreEqual(expected.Name, actual.Name, $"{i}: Name");
                Assert.AreEqual(expected.Width, actual.Width, $"{i}: Width");
                Assert.AreEqual(expected.Height, actual.Height, $"{i}: Height");
                Assert.AreEqual(expected.MaxFrame, actual.MaxFrame, $"{i}: MaxFrame");
                Assert.AreEqual(expected.Cursor, actual.Cursor, $"{i}: Cursor");
                Assert.AreEqual(expected.Zoom, actual.Zoom, $"{i}: Zoom");
                Assert.AreEqual(expected.TimeScroll, actual.TimeScroll, $"{i}: TimeScroll");
                Assert.AreEqual(expected.EditingObject, actual.EditingObject, $"{i}: EditingObject");
                Assert.AreEqual(expected.SelectedFrameStart, actual.SelectedFrameStart, $"{i}: SelectedFrameStart");
                Assert.AreEqual(expected.SelectedFrameEnd, actual.SelectedFrameEnd, $"{i}: SelectedFrameEnd");
                Assert.AreEqual(expected.EnableBpmGrid, actual.EnableBpmGrid, $"{i}: EnableBpmGrid");
                Assert.AreEqual(expected.BpmGridTempo, actual.BpmGridTempo, $"{i}: BpmGridTempo");
                Assert.AreEqual(expected.BpmGridOffset, actual.BpmGridOffset, $"{i}: BpmGridOffset");
                Assert.AreEqual(expected.EnableXYGrid, actual.EnableXYGrid, $"{i}: EnableXYGrid");
                Assert.AreEqual(expected.XYGridWidth, actual.XYGridWidth, $"{i}: XYGridWidth");
                Assert.AreEqual(expected.XYGridHeight, actual.XYGridHeight, $"{i}: XYGridHeight");
                Assert.AreEqual(expected.EnableCameraGrid, actual.EnableCameraGrid, $"{i}: EnableCameraGrid");
                Assert.AreEqual(expected.CameraGridSize, actual.CameraGridSize, $"{i}: CameraGridSize");
                Assert.AreEqual(expected.CameraGridNum, actual.CameraGridNum, $"{i}: CameraGridNum");
                Assert.AreEqual(expected.ShowOutsideFrame, actual.ShowOutsideFrame, $"{i}: ShowOutsideFrame");
                Assert.AreEqual(expected.OutsideFrameScale, actual.OutsideFrameScale, $"{i}: OutsideFrameScale");
                Assert.AreEqual(expected.BpmGridBeat, actual.BpmGridBeat, $"{i}: BpmGridBeat");
                Assert.AreEqual(expected.LayerScroll, actual.LayerScroll, $"{i}: LayerScroll");
                CollectionAssert.AreEqual(expected.Field0xA0_0xDC, actual.Field0xA0_0xDC, $"{i}: Field0xA0_0xDC");
            }
        }

        [DataTestMethod]
        [DataRow(@"TestData\Exedit\Scene01.dat")]
        public void Test_Dump(string filename)
        {
            var expected = File.ReadAllBytes(filename);
            Scene scene = new Scene(expected);
            var actual = new byte[Scene.Size];
            scene.Dump(actual);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
