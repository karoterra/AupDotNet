using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class MotionBlurFilterEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 5);
            var effect = obj.Effects[7] as MotionBlurFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.Interval.Current, "Interval.Current");
            Assert.AreEqual(10, effect.Resolution.Current, "Resolution.Current");
            Assert.AreEqual(false, effect.HighResOutput, "HighResOutput");

            effect = obj.Effects[8] as MotionBlurFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.HighResOutput, "HighResOutput");
        }
    }
}
