using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class RasterEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 35);
            var effect = obj.Effects[1] as RasterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(100, effect.Width.Current, "Width.Current");
            Assert.AreEqual(101, effect.Height.Current, "Height.Current");
            Assert.AreEqual(102, effect.Period.Current, "Period.Current");
            Assert.AreEqual(true, effect.Vertical, "Vertical");
            Assert.AreEqual(false, effect.Random, "Random");

            effect = obj.Effects[2] as RasterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Vertical, "Vertical");
            Assert.AreEqual(true, effect.Random, "Random");
        }
    }
}
