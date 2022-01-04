using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class ZoomEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 25);
            var effect = obj.Effects[4] as ZoomEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(10000, effect.Zoom.Current, "Zoom.Current");
            Assert.AreEqual(10100, effect.X.Current, "X.Current");
            Assert.AreEqual(10200, effect.Y.Current, "Y.Current");
        }
    }
}
