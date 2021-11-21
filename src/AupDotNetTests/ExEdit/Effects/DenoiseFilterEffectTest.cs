using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class DenoiseFilterEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 10);
            var effect = obj.Effects[11] as DenoiseFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(256, effect.Intensity.Current, "Intensity.Current");
            Assert.AreEqual(2, effect.Size.Current, "Size.Current");
            Assert.AreEqual(24, effect.Threshold.Current, "Threshold.Current");
        }
    }
}
