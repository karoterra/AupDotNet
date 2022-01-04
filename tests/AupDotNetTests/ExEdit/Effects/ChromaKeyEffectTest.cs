using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class ChromaKeyEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 5);
            var effect = obj.Effects[9] as ChromaKeyEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(3, effect.HueRange.Current, "HueRange.Current");
            Assert.AreEqual(2, effect.ChromaRange.Current, "ChromaRange.Current");
            Assert.AreEqual(1, effect.Boundary.Current, "Boundary.Current");
            Assert.AreEqual(true, effect.ColorCorrection, "ColorCorrection");
            Assert.AreEqual(false, effect.TransparencyCorrection, "TransparencyCorrection");
            Assert.AreEqual(0, effect.Status, "Status");

            effect = obj.Effects[10] as ChromaKeyEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.ColorCorrection, "ColorCorrection");
            Assert.AreEqual(true, effect.TransparencyCorrection, "TransparencyCorrection");
            Assert.AreEqual(1, effect.Status, "Status");
            Assert.AreEqual(new YCbCr(2266, -1279, -643), effect.Color, "Color");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var chromaKey = new ChromaKeyEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = chromaKey.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 5);
            CheckDumpExtData(obj.Effects[9] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[10] as CustomEffect, "2");
        }
    }
}
