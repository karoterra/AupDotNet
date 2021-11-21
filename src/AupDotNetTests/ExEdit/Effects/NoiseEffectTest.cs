using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class NoiseEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 40);
            var effect = obj.Effects[2] as NoiseEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(10, effect.Intensity.Current, "Intensity.Current");
            Assert.AreEqual(20, effect.SpeedX.Current, "SpeedX.Current");
            Assert.AreEqual(30, effect.SpeedY.Current, "SpeedY.Current");
            Assert.AreEqual(40, effect.NoiseSpeed.Current, "NoiseSpeed.Current");
            Assert.AreEqual(500, effect.PeriodX.Current, "PeriodX.Current");
            Assert.AreEqual(600, effect.PeriodY.Current, "PeriodY.Current");
            Assert.AreEqual(70, effect.Threshold.Current, "Threshold.Current");
            Assert.AreEqual(5, effect.NoiseType, "NoiseType");
            Assert.AreEqual(1, effect.Mode, "Mode");
            Assert.AreEqual(100, effect.Seed, "Seed");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var noise = new NoiseEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = noise.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 40);
            CheckDumpExtData(obj.Effects[2] as CustomEffect, "1");
        }
    }
}
