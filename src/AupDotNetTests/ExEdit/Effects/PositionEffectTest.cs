using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class PositionEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 25);
            var effect = obj.Effects[3] as PositionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.X.Current, "X.Current");
            Assert.AreEqual(2, effect.Y.Current, "Y.Current");
            Assert.AreEqual(3, effect.Z.Current, "Z.Current");
        }
    }
}
