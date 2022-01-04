using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class DirectionalBlurEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 20);
            var effect = obj.Effects[6] as DirectionalBlurEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(20, effect.Size.Current, "Size.Current");
            Assert.AreEqual(500, effect.Angle.Current, "Angle.Current");
            Assert.AreEqual(false, effect.FixSize, "FixSize");

            effect = obj.Effects[7] as DirectionalBlurEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.FixSize, "FixSize");
        }
    }
}
