using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class BlurEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 0);
            var effect = obj.Effects[5] as BlurEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(18, effect.Size.Current, "Size.Current");
            Assert.AreEqual(19, effect.AspectRatio.Current, "AspectRatio.Current");
            Assert.AreEqual(20, effect.Intensity.Current, "Intensity.Current");
            Assert.AreEqual(false, effect.FixSize, "FixSize");

            effect = obj.Effects[6] as BlurEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.FixSize, "FixSize");
        }
    }
}
