using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class AreaExpansionEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 25);
            var effect = obj.Effects[7] as AreaExpansionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.Top.Current, "Top.Current");
            Assert.AreEqual(2, effect.Bottom.Current, "Bottom.Current");
            Assert.AreEqual(3, effect.Left.Current, "Left.Current");
            Assert.AreEqual(4, effect.Right.Current, "Right.Current");
            Assert.AreEqual(false, effect.Fill, "Fill");

            effect = obj.Effects[8] as AreaExpansionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.Fill, "Fill");
        }
    }
}
