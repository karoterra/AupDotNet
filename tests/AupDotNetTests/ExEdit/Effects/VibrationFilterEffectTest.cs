using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class VibrationFilterEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 5);
            var effect = obj.Effects[9] as VibrationFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.X.Current, "X.Current");
            Assert.AreEqual(2, effect.Y.Current, "Y.Current");
            Assert.AreEqual(3, effect.Z.Current, "Z.Current");
            Assert.AreEqual(4, effect.Period.Current, "Period.Current");
            Assert.AreEqual(true, effect.Random, "Random");
            Assert.AreEqual(false, effect.Complex, "Complex");

            effect = obj.Effects[10] as VibrationFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Random, "Random");
            Assert.AreEqual(true, effect.Complex, "Complex");
        }
    }
}
