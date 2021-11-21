using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class AudioDelayEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 1, 0);
            var effect = obj.Effects[2] as AudioDelayEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(7, effect.Gain.Current, "Gain.Current");
            Assert.AreEqual(8, effect.Delay.Current, "Delay.Current");
        }
    }
}
