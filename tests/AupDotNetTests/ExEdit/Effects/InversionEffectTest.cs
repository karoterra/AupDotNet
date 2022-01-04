using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class InversionEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 30);
            var effect = obj.Effects[2] as InversionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.Vertical, "Vertical");
            Assert.AreEqual(false, effect.Horizontal, "Horizontal");
            Assert.AreEqual(false, effect.Luminance, "Luminance");
            Assert.AreEqual(false, effect.Hue, "Hue");
            Assert.AreEqual(false, effect.Transparency, "Transparency");

            effect = obj.Effects[3] as InversionEffect;
            Assert.AreEqual(false, effect.Vertical, "Vertical");
            Assert.AreEqual(true, effect.Horizontal, "Horizontal");
            Assert.AreEqual(false, effect.Luminance, "Luminance");
            Assert.AreEqual(false, effect.Hue, "Hue");
            Assert.AreEqual(false, effect.Transparency, "Transparency");

            effect = obj.Effects[4] as InversionEffect;
            Assert.AreEqual(false, effect.Vertical, "Vertical");
            Assert.AreEqual(false, effect.Horizontal, "Horizontal");
            Assert.AreEqual(true, effect.Luminance, "Luminance");
            Assert.AreEqual(false, effect.Hue, "Hue");
            Assert.AreEqual(false, effect.Transparency, "Transparency");

            effect = obj.Effects[5] as InversionEffect;
            Assert.AreEqual(false, effect.Vertical, "Vertical");
            Assert.AreEqual(false, effect.Horizontal, "Horizontal");
            Assert.AreEqual(false, effect.Luminance, "Luminance");
            Assert.AreEqual(true, effect.Hue, "Hue");
            Assert.AreEqual(false, effect.Transparency, "Transparency");

            effect = obj.Effects[6] as InversionEffect;
            Assert.AreEqual(false, effect.Vertical, "Vertical");
            Assert.AreEqual(false, effect.Horizontal, "Horizontal");
            Assert.AreEqual(false, effect.Luminance, "Luminance");
            Assert.AreEqual(false, effect.Hue, "Hue");
            Assert.AreEqual(true, effect.Transparency, "Transparency");
        }
    }
}
