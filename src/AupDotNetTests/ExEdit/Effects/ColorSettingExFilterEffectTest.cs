using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class ColorSettingExFilterEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 10);
            var effect = obj.Effects[9] as ColorSettingExFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(10, effect.R.Current, "R.Current");
            Assert.AreEqual(20, effect.G.Current, "G.Current");
            Assert.AreEqual(30, effect.B.Current, "B.Current");
            Assert.AreEqual(false, effect.HSV, "HSV");

            effect = obj.Effects[10] as ColorSettingExFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.HSV, "HSV");
        }
    }
}
