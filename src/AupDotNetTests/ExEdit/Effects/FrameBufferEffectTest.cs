using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class FrameBufferEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 60);
            var effect = obj.Effects[0] as FrameBufferEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.ClearBuffer, "ClearBuffer");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 65);
            effect = obj.Effects[0] as FrameBufferEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.ClearBuffer, "ClearBuffer");
        }
    }
}
