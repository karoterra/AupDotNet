using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class BlurFilter2EffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 15);
            var effect = obj.Effects[2] as BlurFilter2Effect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(256, effect.Intensity.Current, "Intensity.Current");
            Assert.AreEqual(2, effect.Size.Current, "Size.Current");
            Assert.AreEqual(1, effect.Lower.Current, "Lower.Current");
            Assert.AreEqual(1024, effect.Upper.Current, "Upper.Current");
        }
    }
}
