using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class MosaicFilterEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 0);
            var effect = obj.Effects[9] as MosaicFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(12, effect.Size.Current, "Size.Current");
            Assert.AreEqual(false, effect.Tile, "Tile");

            effect = obj.Effects[10] as MosaicFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.Tile, "Tile");
        }
    }
}
