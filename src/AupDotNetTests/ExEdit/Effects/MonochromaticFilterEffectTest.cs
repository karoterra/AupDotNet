using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class MonochromaticFilterEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 10);
            var effect = obj.Effects[7] as MonochromaticFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1000, effect.Intensity.Current, "Intensity.Current");
            Assert.AreEqual(Color.FromArgb(30, 40, 50), effect.Color, "Color");
            Assert.AreEqual(true, effect.KeepLuminance, "KeepLuminance");

            effect = obj.Effects[8] as MonochromaticFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(Color.FromArgb(255, 255, 255), effect.Color, "Color");
            Assert.AreEqual(false, effect.KeepLuminance, "KeepLuminance");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var mono = new MonochromaticFilterEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = mono.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 10);
            CheckDumpExtData(obj.Effects[7] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[8] as CustomEffect, "2");
        }
    }
}
