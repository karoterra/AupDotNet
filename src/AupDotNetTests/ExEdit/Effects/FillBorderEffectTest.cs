using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class FillBorderEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 15);
            var effect = obj.Effects[4] as FillBorderEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.Top.Current, "Top.Current");
            Assert.AreEqual(2, effect.Bottom.Current, "Bottom.Current");
            Assert.AreEqual(3, effect.Left.Current, "Left.Current");
            Assert.AreEqual(4, effect.Right.Current, "Right.Current");
            Assert.AreEqual(true, effect.Centering, "Centering");
            Assert.AreEqual(false, effect.EdgeColor, "EdgeColor");

            effect = obj.Effects[5] as FillBorderEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Centering, "Centering");
            Assert.AreEqual(true, effect.EdgeColor, "EdgeColor");
        }
    }
}
