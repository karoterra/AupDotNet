using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class ClippingEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 0);
            var effect = obj.Effects[3] as ClippingEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(14, effect.Top.Current, "Top.Current");
            Assert.AreEqual(15, effect.Bottom.Current, "Bottom.Current");
            Assert.AreEqual(16, effect.Left.Current, "Left.Current");
            Assert.AreEqual(17, effect.Right.Current, "Right.Current");
            Assert.AreEqual(false, effect.Centering, "Centering");

            effect = obj.Effects[4] as ClippingEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.Centering, "Centering");
        }
    }
}
