using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class MotionBlurEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 20);
            var effect = obj.Effects[10] as MotionBlurEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.Interval.Current, "Interval.Current");
            Assert.AreEqual(10, effect.Resolution.Current, "Resolution.Current");
            Assert.AreEqual(false, effect.Afterimage, "Afterimage");
            Assert.AreEqual(true, effect.Offscreen, "Offscreen");
            Assert.AreEqual(false, effect.HighResOutput, "HighResOutput");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 25);
            effect = obj.Effects[1] as MotionBlurEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.Afterimage, "Afterimage");
            Assert.AreEqual(false, effect.Offscreen, "Offscreen");
            Assert.AreEqual(false, effect.HighResOutput, "HighResOutput");

            effect = obj.Effects[2] as MotionBlurEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Afterimage, "Afterimage");
            Assert.AreEqual(false, effect.Offscreen, "Offscreen");
            Assert.AreEqual(true, effect.HighResOutput, "HighResOutput");
        }
    }
}
