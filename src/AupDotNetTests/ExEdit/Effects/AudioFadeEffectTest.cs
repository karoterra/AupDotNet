using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class AudioFadeEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 1, 0);
            var effect = obj.Effects[1] as AudioFadeEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(5, effect.In.Current, "In.Current");
            Assert.AreEqual(6, effect.Out.Current, "Out.Current");
        }
    }
}
