using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class DiffuseFilterEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 5);
            var effect = obj.Effects[1] as DiffuseFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(500, effect.Intensity.Current, "Intensity.Current");
            Assert.AreEqual(12, effect.Diffusion.Current, "Diffusion.Current");
        }
    }
}
