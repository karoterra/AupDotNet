using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class LensBlurEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 20);
            var effect = obj.Effects[8] as LensBlurEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(5, effect.Size.Current, "Size.Current");
            Assert.AreEqual(32, effect.Intensity.Current, "Intensity.Current");
            Assert.AreEqual(true, effect.FixSize, "FixSize");

            effect = obj.Effects[9] as LensBlurEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.FixSize, "FixSize");
        }
    }
}
