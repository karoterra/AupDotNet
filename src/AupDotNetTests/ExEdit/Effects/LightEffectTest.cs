using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class LightEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 10);
            var effect = obj.Effects[5] as LightEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1000, effect.Intensity.Current, "Intensity.Current");
            Assert.AreEqual(25, effect.Diffusion.Current, "Diffusion.Current");
            Assert.AreEqual(0, effect.Rate.Current, "Rate.Current");
            Assert.AreEqual(false, effect.Backlight, "Backlight");
            Assert.AreEqual(Color.FromArgb(255, 255, 255, 255), effect.Color, "Color");

            effect = obj.Effects[6] as LightEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.Backlight, "Backlight");
            Assert.AreEqual(Color.FromArgb(0, 156, 43, 35), effect.Color, "Color");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var light = new LightEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = light.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 10);
            CheckDumpExtData(obj.Effects[5] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[6] as CustomEffect, "2");
        }
    }
}
