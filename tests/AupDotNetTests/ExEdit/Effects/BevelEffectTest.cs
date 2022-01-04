using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class BevelEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 15);
            var effect = obj.Effects[1] as BevelEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(4, effect.Width.Current, "Width.Current");
            Assert.AreEqual(100, effect.Height.Current, "Height.Current");
            Assert.AreEqual(-450, effect.Angle.Current, "Angle.Current");
        }
    }
}
