using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class AudioDelayFilterEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 3, 0);
            var effect = obj.Effects[0] as AudioDelayFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(500, effect.Gain.Current, "Gain.Current");
            Assert.AreEqual(100, effect.Delay.Current, "Delay.Current");
        }
    }
}
