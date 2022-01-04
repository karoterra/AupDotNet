using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class EdgeExtractionEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 15);
            var effect = obj.Effects[2] as EdgeExtractionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1000, effect.Intensity.Current, "Intensity.Current");
            Assert.AreEqual(0, effect.Threshold.Current, "Threshold.Current");
            Assert.AreEqual(true, effect.LuminanceEdge, "LuminanceEdge");
            Assert.AreEqual(false, effect.TransparencyEdge, "TransparencyEdge");
            Assert.AreEqual(Color.FromArgb(0, 255, 255, 255), effect.Color, "Color");

            effect = obj.Effects[3] as EdgeExtractionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.LuminanceEdge, "LuminanceEdge");
            Assert.AreEqual(true, effect.TransparencyEdge, "TransparencyEdge");
            Assert.AreEqual(Color.FromArgb(0, 26, 113, 19), effect.Color, "Color");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var ed = new EdgeExtractionEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = ed.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 15);
            CheckDumpExtData(obj.Effects[2] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[3] as CustomEffect, "2");
        }
    }
}
