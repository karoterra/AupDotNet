using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class ColorSettingExEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 40);
            var effect = obj.Effects[8] as ColorSettingExEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(10, effect.R.Current, "R.Current");
            Assert.AreEqual(20, effect.G.Current, "G.Current");
            Assert.AreEqual(30, effect.B.Current, "B.Current");
            Assert.AreEqual(false, effect.HSV, "HSV");

            effect = obj.Effects[9] as ColorSettingExEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(40, effect.R.Current, "R.Current");
            Assert.AreEqual(50, effect.G.Current, "G.Current");
            Assert.AreEqual(60, effect.B.Current, "B.Current");
            Assert.AreEqual(true, effect.HSV, "HSV");
        }
    }
}
