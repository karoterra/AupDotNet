using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class GamutConversionEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 40);
            var effect = obj.Effects[10] as GamutConversionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.Hue.Current, "Hue.Current");
            Assert.AreEqual(2, effect.Chroma.Current, "Chroma.Current");
            Assert.AreEqual(3, effect.Border.Current, "Border.Current");
            Assert.AreEqual(true, effect.Status, "Status");
            Assert.AreEqual(new YCbCr(2625, 720, 270), effect.Color, "Color");
            Assert.AreEqual(false, effect.Status2, "Status2");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 45);
            effect = obj.Effects[1] as GamutConversionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Status, "Status");
            Assert.AreEqual(true, effect.Status2, "Status2");
            Assert.AreEqual(new YCbCr(2289, -648, 1289), effect.Color2, "Color2");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var gamut = new GamutConversionEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = gamut.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 40);
            CheckDumpExtData(obj.Effects[10] as CustomEffect, "1");
            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 45);
            CheckDumpExtData(obj.Effects[1] as CustomEffect, "2");
        }
    }
}
