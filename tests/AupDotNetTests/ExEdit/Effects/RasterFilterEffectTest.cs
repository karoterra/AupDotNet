using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class RasterFilterEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 10);
            var effect = obj.Effects[3] as RasterFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(100, effect.Width.Current, "Width.Current");
            Assert.AreEqual(200, effect.Height.Current, "Height.Current");
            Assert.AreEqual(300, effect.Period.Current, "Period.Current");
            Assert.AreEqual(true, effect.Vertical, "Vertical");
            Assert.AreEqual(false, effect.Random, "Random");

            effect = obj.Effects[4] as RasterFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Vertical, "Vertical");
            Assert.AreEqual(true, effect.Random, "Random");
        }
    }
}
