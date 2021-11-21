using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class StandardPlaybackEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 1, 0);
            var effect = obj.Effects[4] as StandardPlaybackEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.Volume.Current, "Volume.Current");
            Assert.AreEqual(2, effect.Pan.Current, "Pan.Current");
        }
    }
}
