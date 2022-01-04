using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class ColorCorrectionFilterEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 0);
            var effect = obj.Effects[6] as ColorCorrectionFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(10, effect.Brightness.Current, "Brightness.Current");
            Assert.AreEqual(20, effect.Contrast.Current, "Contrast.Current");
            Assert.AreEqual(30, effect.Hue.Current, "Hue.Current");
            Assert.AreEqual(40, effect.Luminance.Current, "Luminance.Current");
            Assert.AreEqual(50, effect.Chroma.Current, "Chroma.Current");
            Assert.AreEqual(false, effect.Saturation, "Saturation");

            effect = obj.Effects[7] as ColorCorrectionFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.Saturation, "Saturation");
        }
    }
}
