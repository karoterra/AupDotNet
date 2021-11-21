using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class ColorCorrectionExEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 15);
            var effect = obj.Effects[7] as ColorCorrectionExEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.OffsetY.Current, "OffsetY.Current");
            Assert.AreEqual(2, effect.GainY.Current, "GainY.Current");
            Assert.AreEqual(3, effect.OffsetCb.Current, "OffsetCb.Current");
            Assert.AreEqual(4, effect.GainCb.Current, "GainCb.Current");
            Assert.AreEqual(5, effect.OffsetCr.Current, "OffsetCr.Current");
            Assert.AreEqual(6, effect.GainCr.Current, "GainCr.Current");
            Assert.AreEqual(7, effect.OffsetR.Current, "OffsetR.Current");
            Assert.AreEqual(8, effect.GainR.Current, "GainR.Current");
            Assert.AreEqual(9, effect.GammaR.Current, "GammaR.Current");
            Assert.AreEqual(10, effect.OffsetG.Current, "OffsetG.Current");
            Assert.AreEqual(11, effect.GainG.Current, "GainG.Current");
            Assert.AreEqual(12, effect.GammaG.Current, "GammaG.Current");
            Assert.AreEqual(13, effect.OffsetB.Current, "OffsetB.Current");
            Assert.AreEqual(14, effect.GainB.Current, "GainB.Current");
            Assert.AreEqual(15, effect.GammaB.Current, "GammaB.Current");
            Assert.AreEqual(true, effect.SyncRGB, "SyncRGB");
            Assert.AreEqual(false, effect.TVtoPC, "TVtoPC");
            Assert.AreEqual(false, effect.PCtoTV, "PCtoTV");

            effect = obj.Effects[8] as ColorCorrectionExEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.SyncRGB, "SyncRGB");
            Assert.AreEqual(true, effect.TVtoPC, "TVtoPC");
            Assert.AreEqual(false, effect.PCtoTV, "PCtoTV");

            obj = ExeditTestUtil.GetObject(exedit, 0, 2, 20);
            effect = obj.Effects[1] as ColorCorrectionExEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.SyncRGB, "SyncRGB");
            Assert.AreEqual(false, effect.TVtoPC, "TVtoPC");
            Assert.AreEqual(true, effect.PCtoTV, "PCtoTV");
        }
    }
}
