using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class EmissionEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 5);
            var effect = obj.Effects[1] as EmissionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.Intensity.Current, "Intensity.Current");
            Assert.AreEqual(10, effect.Diffusion.Current, "Diffusion.Current");
            Assert.AreEqual(3, effect.Threshold.Current, "Threshold.Current");
            Assert.AreEqual(4, effect.DiffusionRate.Current, "DiffusionRate.Current");
            Assert.AreEqual(false, effect.FixSize, "FixSize");
            Assert.AreEqual(true, effect.NoColor, "NoColor");

            effect = obj.Effects[2] as EmissionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.FixSize, "FixSize");
            Assert.AreEqual(false, effect.NoColor, "NoColor");
            Assert.AreEqual(Color.FromArgb(10, 20, 30), effect.Color, "Color");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var emission = new EmissionEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = emission.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 5);
            CheckDumpExtData(obj.Effects[1] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[2] as CustomEffect, "2");
        }
    }
}
