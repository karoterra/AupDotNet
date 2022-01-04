using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class RippleEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 35);
            var effect = obj.Effects[3] as RippleEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.X.Current, "X.Current");
            Assert.AreEqual(2, effect.Y.Current, "Y.Current");
            Assert.AreEqual(300, effect.Wavelength.Current, "Wavelength.Current");
            Assert.AreEqual(150, effect.Amplitude.Current, "Amplitude.Current");
            Assert.AreEqual(1500, effect.Speed.Current, "Speed.Current");
            Assert.AreEqual(1, effect.Num, "Num");
            Assert.AreEqual(2, effect.Interval, "Interval");
            Assert.AreEqual(3, effect.Add, "Add");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var ripple = new RippleEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = ripple.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 35);
            CheckDumpExtData(obj.Effects[3] as CustomEffect, "1");
        }
    }
}
