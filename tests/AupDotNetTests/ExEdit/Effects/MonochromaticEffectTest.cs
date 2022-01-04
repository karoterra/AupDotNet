using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class MonochromaticEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 40);
            var effect = obj.Effects[4] as MonochromaticEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1000, effect.Intensity.Current, "Intensity.Current");
            Assert.AreEqual(true, effect.KeepLuminance, "KeepLuminance");
            Assert.AreEqual(Color.FromArgb(10, 20, 30), effect.Color, "Color");

            effect = obj.Effects[5] as MonochromaticEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.KeepLuminance, "KeepLuminance");
            Assert.AreEqual(Color.FromArgb(255, 255, 255), effect.Color, "Color");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var mono = new MonochromaticEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = mono.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 40);
            CheckDumpExtData(obj.Effects[4] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[5] as CustomEffect, "2");
        }
    }
}
