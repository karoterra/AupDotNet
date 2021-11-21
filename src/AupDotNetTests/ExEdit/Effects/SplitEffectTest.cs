using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class SplitEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 50);
            var effect = obj.Effects[9] as SplitEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.Horizontal.Current, "Horizontal.Current");
            Assert.AreEqual(2, effect.Vertical.Current, "Vertical.Current");
        }
    }
}
