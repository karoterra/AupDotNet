using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class CameraOptionEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 50);
            var effect = obj.Effects[4] as CameraOptionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.Billboard, "Billboard");
            Assert.AreEqual(false, effect.BillboardVH, "BillboardVH");
            Assert.AreEqual(false, effect.BillboardH, "BillboardH");
            Assert.AreEqual(false, effect.NoShadow, "NoShadow");

            effect = obj.Effects[5] as CameraOptionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Billboard, "Billboard");
            Assert.AreEqual(true, effect.BillboardVH, "BillboardVH");
            Assert.AreEqual(false, effect.BillboardH, "BillboardH");
            Assert.AreEqual(false, effect.NoShadow, "NoShadow");

            effect = obj.Effects[6] as CameraOptionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Billboard, "Billboard");
            Assert.AreEqual(false, effect.BillboardVH, "BillboardVH");
            Assert.AreEqual(true, effect.BillboardH, "BillboardH");
            Assert.AreEqual(false, effect.NoShadow, "NoShadow");

            effect = obj.Effects[7] as CameraOptionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Billboard, "Billboard");
            Assert.AreEqual(false, effect.BillboardVH, "BillboardVH");
            Assert.AreEqual(false, effect.BillboardH, "BillboardH");
            Assert.AreEqual(true, effect.NoShadow, "NoShadow");
        }
    }
}
