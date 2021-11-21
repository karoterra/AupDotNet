using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class GlowEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 5);
            var effect = obj.Effects[7] as GlowEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(10, effect.Intensity.Current, "Intensity.Current");
            Assert.AreEqual(11, effect.Diffusion.Current, "Diffusion.Current");
            Assert.AreEqual(12, effect.Threshold.Current, "Threshold.Current");
            Assert.AreEqual(13, effect.Blur.Current, "Blur.Current");
            Assert.AreEqual(0, effect.ShapeType, "ShapeType");
            Assert.AreEqual(true, effect.NoColor, "NoColor");

            effect = obj.Effects[8] as GlowEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(5, effect.ShapeType, "ShapeType");
            Assert.AreEqual(false, effect.NoColor, "NoColor");
            Assert.AreEqual(Color.FromArgb(30, 40, 50), effect.Color, "Color");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var glow = new GlowEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = glow.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 5);
            CheckDumpExtData(obj.Effects[7] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[8] as CustomEffect, "2");
        }
    }
}
