using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class ColorCorrectionEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 0);
            var effect = obj.Effects[1] as ColorCorrectionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(9, effect.Brightness.Current, "Brightness.Current");
            Assert.AreEqual(10, effect.Contrast.Current, "Contrast.Current");
            Assert.AreEqual(11, effect.Hue.Current, "Hue.Current");
            Assert.AreEqual(12, effect.Luminance.Current, "Luminance.Current");
            Assert.AreEqual(13, effect.Chroma.Current, "Chroma.Current");
            Assert.AreEqual(false, effect.Saturation, "Saturation");

            effect = obj.Effects[2] as ColorCorrectionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.Saturation, "Saturation");
        }
    }
}
