using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class GlowFilterEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 5);
            var effect = obj.Effects[2] as GlowFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(400, effect.Intensity.Current, "Intensity.Current");
            Assert.AreEqual(30, effect.Diffusion.Current, "Diffusion.Current");
            Assert.AreEqual(800, effect.Threshold.Current, "Threshold.Current");
            Assert.AreEqual(1, effect.Blur.Current, "Blur.Current");
            Assert.AreEqual(0, effect.ShapeType, "ShapeType");
            Assert.AreEqual(true, effect.NoColor, "NoColor");

            effect = obj.Effects[3] as GlowFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(4, effect.ShapeType, "ShapeType");
            Assert.AreEqual(false, effect.NoColor, "NoColor");
            Assert.AreEqual(Color.FromArgb(10, 20, 30), effect.Color, "Color");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var glow = new GlowFilterEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = glow.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 5);
            CheckDumpExtData(obj.Effects[2] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[3] as CustomEffect, "2");
        }
    }
}
