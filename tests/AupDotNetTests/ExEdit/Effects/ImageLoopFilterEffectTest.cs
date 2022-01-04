using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class ImageLoopFilterEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 10);
            var effect = obj.Effects[5] as ImageLoopFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(10, effect.NumX.Current, "NumX.Current");
            Assert.AreEqual(20, effect.NumY.Current, "NumY.Current");
            Assert.AreEqual(30, effect.SpeedX.Current, "SpeedX.Current");
            Assert.AreEqual(40, effect.SpeedY.Current, "SpeedY.Current");
        }
    }
}
