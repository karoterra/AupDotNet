using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class PolarTransformEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 35);
            var effect = obj.Effects[6] as PolarTransformEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.Margin.Current, "Margin.Current");
            Assert.AreEqual(1000, effect.Zoom.Current, "Zoom.Current");
            Assert.AreEqual(20, effect.Rotation.Current, "Rotation.Current");
            Assert.AreEqual(300, effect.Spiral.Current, "Spiral.Current");
        }
    }
}
