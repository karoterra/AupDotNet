using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class BlurFilterEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 0);
            var effect = obj.Effects[8] as BlurFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.Size.Current, "Size.Current");
            Assert.AreEqual(20, effect.AspectRatio.Current, "AspectRatio.Current");
            Assert.AreEqual(3, effect.Intensity.Current, "Intensity.Current");
        }
    }
}
