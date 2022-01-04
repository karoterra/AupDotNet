using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class EmissionFilterEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 0);
            var effect = obj.Effects[11] as EmissionFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1000, effect.Intensity.Current, "Intensity.Current");
            Assert.AreEqual(250, effect.Diffusion.Current, "Diffusion.Current");
            Assert.AreEqual(800, effect.Threshold.Current, "Threshold.Current");
            Assert.AreEqual(5, effect.DiffusionRate.Current, "DiffusionRate.Current");
            Assert.AreEqual(true, effect.NoColor, "NoColor");

            obj = ExeditTestUtil.GetObject(exedit, 0, 2, 5);
            effect = obj.Effects[0] as EmissionFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.NoColor, "NoColor");
            Assert.AreEqual(Color.FromArgb(10, 20, 30), effect.Color, "Color");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var emission = new EmissionFilterEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = emission.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 0);
            CheckDumpExtData(obj.Effects[11] as CustomEffect, "1");
            obj = ExeditTestUtil.GetObject(exedit, 0, 2, 5);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "2");
        }
    }
}
