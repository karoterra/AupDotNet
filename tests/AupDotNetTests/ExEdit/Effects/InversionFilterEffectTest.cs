using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class InversionFilterEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 5);
            var effect = obj.Effects[11] as InversionFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.Vertical, "Vertical");
            Assert.AreEqual(false, effect.Horizontal, "Horizontal");
            Assert.AreEqual(false, effect.Luminance, "Luminance");
            Assert.AreEqual(false, effect.Hue, "Hue");

            obj = ExeditTestUtil.GetObject(exedit, 0, 2, 10);
            effect = obj.Effects[0] as InversionFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Vertical, "Vertical");
            Assert.AreEqual(true, effect.Horizontal, "Horizontal");
            Assert.AreEqual(false, effect.Luminance, "Luminance");
            Assert.AreEqual(false, effect.Hue, "Hue");

            effect = obj.Effects[1] as InversionFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Vertical, "Vertical");
            Assert.AreEqual(false, effect.Horizontal, "Horizontal");
            Assert.AreEqual(true, effect.Luminance, "Luminance");
            Assert.AreEqual(false, effect.Hue, "Hue");

            effect = obj.Effects[2] as InversionFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Vertical, "Vertical");
            Assert.AreEqual(false, effect.Horizontal, "Horizontal");
            Assert.AreEqual(false, effect.Luminance, "Luminance");
            Assert.AreEqual(true, effect.Hue, "Hue");
        }
    }
}
