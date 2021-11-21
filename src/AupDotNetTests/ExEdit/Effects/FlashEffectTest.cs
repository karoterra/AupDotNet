using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class FlashEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 5);
            var effect = obj.Effects[3] as FlashEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(5, effect.Intensity.Current, "Intensity.Current");
            Assert.AreEqual(6, effect.X.Current, "X.Current");
            Assert.AreEqual(7, effect.Y.Current, "Y.Current");
            Assert.AreEqual(false, effect.FixSize, "FixSize");
            Assert.AreEqual(true, effect.NoColor, "NoColor");
            Assert.AreEqual(0, effect.Mode, "Mode");

            effect = obj.Effects[4] as FlashEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.FixSize, "FixSize");
            Assert.AreEqual(false, effect.NoColor, "NoColor");
            Assert.AreEqual(Color.FromArgb(10, 20, 30), effect.Color, "Color");
            Assert.AreEqual(2, effect.Mode, "Mode");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var flash = new FlashEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = flash.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 5);
            CheckDumpExtData(obj.Effects[3] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[4] as CustomEffect, "2");
        }
    }
}
