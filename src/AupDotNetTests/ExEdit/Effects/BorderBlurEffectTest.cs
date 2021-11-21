using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class BorderBlurEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 0);
            var effect = obj.Effects[7] as BorderBlurEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(21, effect.Size.Current, "Size.Current");
            Assert.AreEqual(22, effect.AspectRatio.Current, "AspectRatio.Current");
            Assert.AreEqual(false, effect.BlurAlpha, "BlurAlpha");

            effect = obj.Effects[8] as BorderBlurEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.BlurAlpha, "BlurAlpha");
        }
    }
}
