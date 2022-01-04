using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class ColorCorrection2EffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 15);
            var effect = obj.Effects[6] as ColorCorrection2Effect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.Brightness.Current, "Brightness.Current");
            Assert.AreEqual(2, effect.Contrast.Current, "Contrast.Current");
            Assert.AreEqual(3, effect.Gamma.Current, "Gamma.Current");
            Assert.AreEqual(4, effect.Luminance.Current, "Luminance.Current");
            Assert.AreEqual(5, effect.Depth.Current, "Depth.Current");
            Assert.AreEqual(6, effect.Tone.Current, "Tone.Current");
        }
    }
}
