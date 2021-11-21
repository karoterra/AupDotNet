using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class VibrationEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 30);
            var effect = obj.Effects[7] as VibrationEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(10, effect.X.Current, "X.Current");
            Assert.AreEqual(20, effect.Y.Current, "Y.Current");
            Assert.AreEqual(30, effect.Z.Current, "Z.Current");
            Assert.AreEqual(5, effect.Period.Current, "Period.Current");
            Assert.AreEqual(true, effect.Random, "Random");
            Assert.AreEqual(false, effect.Complex, "Complex");

            effect = obj.Effects[8] as VibrationEffect;
            Assert.AreEqual(false, effect.Random, "Random");
            Assert.AreEqual(true, effect.Complex, "Complex");
        }
    }
}
