using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class CameraShadowEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 4, 20);
            var effect = obj.Effects[1] as CameraShadowEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(10, effect.X.Current, "X.Current");
            Assert.AreEqual(20, effect.Y.Current, "Y.Current");
            Assert.AreEqual(30, effect.Z.Current, "Z.Current");
            Assert.AreEqual(40, effect.Depth.Current, "Depth.Current");
            Assert.AreEqual(50, effect.Precision.Current, "Precision.Current");
        }
    }
}
