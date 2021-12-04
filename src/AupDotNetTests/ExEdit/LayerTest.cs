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
    public class LayerTest
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
                Path.GetFileNameWithoutExtension(filename) + "_layer.json");
            string jsonText = File.ReadAllText(jsonPath);
            List<Layer> layers = JsonSerializer.Deserialize<List<Layer>>(jsonText);

            Assert.AreEqual(layers.Count, exedit.Layers.Count);
            for (int i = 0; i < layers.Count; i++)
            {
                Assert.AreEqual(layers[i].SceneIndex, exedit.Layers[i].SceneIndex, $"{i}: SceneIndex");
                Assert.AreEqual(layers[i].LayerIndex, exedit.Layers[i].LayerIndex, $"{i}: LayerIndex");
                Assert.AreEqual(layers[i].Flag, exedit.Layers[i].Flag, $"{i}: Flag");
                Assert.AreEqual(layers[i].Name, exedit.Layers[i].Name, $"{i}: Name");
            }
        }

        [DataTestMethod]
        [DataRow(@"TestData\Exedit\Layer01.dat")]
        public void Test_Dump(string filename)
        {
            var expected = File.ReadAllBytes(filename);
            Layer layer = new Layer(expected);
            var actual = new byte[Layer.Size];
            layer.Dump(actual);
            expected.SequenceEqual(actual);
        }
    }
}
